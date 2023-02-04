using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunTurret : MonoBehaviour
{
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardforce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize;
    public int bulletsPerTap; //Вылетает пуль за выстрел
    public bool allowButtonHold; //Можно зажать мышку чтобы стрелять

    private int bulletsLeft, bulletsShot;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //States
    private bool shooting, readyToShoot, reloading;

    //Reference
    private Camera camera;
    [SerializeField] private Transform attackPoint;
    private Turret Turret;

    public bool allowInvoke;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Start()
    {
        Turret = GetComponent<Turret>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (!Turret.turretUnderControl)
        {
            return;
        }
        
        MyInput();

        if (ammunitionDisplay != null)
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (Turret.turretUnderControl && Input.GetKeyDown(KeyCode.Space))
        {
            RemoveTurret();
            return;
        }

        // if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        if (readyToShoot && shooting && !reloading)
        {
            if (bulletsLeft <= 0)
            {
                Reload();
            }
            else
            {
                bulletsShot = 0;

                Shoot();
            }
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75);

        Vector3 direction = targetPoint - attackPoint.position;
        
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = direction.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(attackPoint.rotation * new Vector3(0,1,0) * shootForce, ForceMode.Impulse);
        
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft-=2;
        bulletsShot+=2;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        RemoveTurret();
        Invoke("ReloadFinished", reloadTime);
    }

    private void RemoveTurret()
    {
        Turret.DoDigOut();
        TurretSpawner.INSTANT.SpawnNext();
        Destroy(gameObject); 
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}