using System;
using System.Collections.Generic;
using UnityEngine;

public class FlowerTurret : MonoBehaviour
{
    private float range = 20f;
    private float maxReloadTime = 20f;
    private float curReloadTime;

    private Turret turret;

    private void Start()
    {
        turret = GetComponent<Turret>();
    }

    private void Update()
    {
        if (!turret.turretUnderControl)
        {
            return;
        }
        Reload();
        Shoot();
    }

    private void Shoot()
    {
        if (curReloadTime > 0) return;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Бум");
            foreach (var enemy in GetEnemyInRange())
            {
                enemy.Scare();
            }
            turret.player.DoDigOut();
        }
    }

    private void Reload()
    {
        if (curReloadTime > 0) curReloadTime -= Time.deltaTime;
        if (curReloadTime < 0) curReloadTime = 0;
    }

    private List<Enemy> GetEnemyInRange()
    {
        GameObject[] enemis = GameObject.FindGameObjectsWithTag("Enemy");
        var result = new List<Enemy>();
        foreach (var enemy in enemis)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range)
            {
                result.Add(enemy.GetComponent<Enemy>());
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