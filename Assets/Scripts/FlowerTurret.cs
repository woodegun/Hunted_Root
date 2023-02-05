using System.Collections.Generic;
using UnityEngine;

public class FlowerTurret : MonoBehaviour
{
    public float range = 20f;
    private float reloadTime = 20f;
    private float curReloadTime;
    private PlayerController Player;
    public GameObject UnactiveFlower;
    public GameObject ActiveFlower;

    //private Turret turret;
    private string hint = "Use the mouse to interact";

    private void Start()
    {
        UnactiveFlower.SetActive(true);
        ActiveFlower.SetActive(false);
        //turret = GetComponent<Turret>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player = other.GetComponent<PlayerController>();
        if (Player != null)
            Player.ShowHint(hint);
    }

    private void Update()
    {
        //if (!turret.turretUnderControl)
        //{
        //    return;
        //}
        Reload();
        Shoot();
    }

    private void Shoot()
    {
        if (curReloadTime > 0) return;

        if (Player != null && Input.GetKeyDown(KeyCode.Mouse0))
        {
            UnactiveFlower.SetActive(false);
            ActiveFlower.SetActive(true);

            Debug.Log("Бум");
            foreach (var enemy in GetEnemyInRange())
            {
                enemy.Scare();
            }

            curReloadTime = reloadTime;
            //turret.player.DoDigOut();
        }
    }

    private void Reload()
    {
        if (curReloadTime > 0)
        {
            curReloadTime -= Time.deltaTime;
        }

        if (curReloadTime <= 0)
        {
            UnactiveFlower.SetActive(true);
            ActiveFlower.SetActive(false);
            curReloadTime = 0;
        }
    }

    private List<EnemyBehaviour> GetEnemyInRange()
    {
        GameObject[] enemis = GameObject.FindGameObjectsWithTag("Enemy");
        var result = new List<EnemyBehaviour>();
        foreach (var enemy in enemis)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                result.Add(enemy.GetComponent<EnemyBehaviour>());
            }
        }

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}