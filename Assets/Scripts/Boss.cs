using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class Boss : MonoBehaviour
{
    private float curHP;
    private float maxHP = 100;

    private Transform _player;

    [SerializeField] private List<Transform> SpawnPoints;
    [SerializeField] private List<Transform> RootsSpawnPoints;

    private float curSpawnTime;
    [SerializeField] private float SpawnTime1Phase = 7;
    [SerializeField] private float SpawnTime2Phase = 8;
    [SerializeField] private float SpawnTime3Phase = 9;
    [SerializeField] private float SpawnTime4Phase = 10;

    [SerializeField] private GameObject Roots;
    [SerializeField] private GameObject Skeleton;
    [SerializeField] private GameObject Ghost;
    [SerializeField] private GameObject Spider;

    private BossPhase BossPhase;

    [SerializeField] private GameObject treeBullet;
    [SerializeField] private GameObject shootPoint;
    private float curShootRate;
    private float maxShootRate;
    [SerializeField] private float ShootRate1Phase = 8;
    [SerializeField] private float ShootRate2Phase = 7;
    [SerializeField] private float ShootRate3Phase = 6;
    [SerializeField] private float ShootRate4Phase = 5;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        curHP = maxHP;
        BossPhase = BossPhase.First;
        maxShootRate = ShootRate1Phase;
    }

    private void FixedUpdate()
    {
        transform.LookAt(_player);
        SpawnTimer();
        PhaseUpdate();
        Shoot();
        switch (BossPhase)
        {
            case BossPhase.First:
                Phase1();
                break;
            case BossPhase.Second:
                Phase2();
                break;
            case BossPhase.Third:
                Phase3();
                break;
            case BossPhase.Fourth:
                Phase4();
                break;
            case BossPhase.Die:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Shoot()
    {
        if (curShootRate > 0)
        {
            curShootRate -= Time.deltaTime;
            return;
        }

        Instantiate(treeBullet, shootPoint.transform.position, shootPoint.transform.rotation);
        curShootRate = maxShootRate;
    }

    private void Phase1()
    {
        if (curSpawnTime <= 0)
        {
            curSpawnTime = SpawnTime1Phase;
            SpawnRoots();
        }
    }

    private void Phase2()
    {
        if (curSpawnTime <= 0)
        {
            curSpawnTime = SpawnTime2Phase;
            SpawnRoots();
            SpawnMonster(Skeleton);
        }
    }

    private void Phase3()
    {
        if (curSpawnTime <= 0)
        {
            curSpawnTime = SpawnTime4Phase;
            SpawnRoots();
            SpawnMonster(Spider);
        }
    }

    private void Phase4()
    {
        if (curSpawnTime <= 0)
        {
            curSpawnTime = SpawnTime3Phase;
            SpawnMonster(Ghost);
        }
    }

    private void PhaseUpdate()
    {
        if (BossPhase == BossPhase.Fourth)
        {
            return;
        }

        var value = curHP / maxHP;
        if (value <= 0.25)
        {
            maxShootRate = ShootRate4Phase;
            BossPhase = BossPhase.Fourth;
            return;
        }

        if (value <= 0.5)
        {
            maxShootRate = ShootRate3Phase;
            BossPhase = BossPhase.Third;
            return;
        }

        if (value <= 0.75)
        {
            maxShootRate = ShootRate2Phase;
            BossPhase = BossPhase.Second;
        }
    }

    private void SpawnTimer()
    {
        if (curSpawnTime <= 0)
        {
            curSpawnTime = 0;
        }
        else
        {
            curSpawnTime -= Time.deltaTime;
        }
    }

    private void SpawnMonster(GameObject monster)
    {
        var spawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];
        Instantiate(monster, spawnPoint.position, Quaternion.identity);
    }

    private void SpawnRoots()
    {
        if (RootsSpawnPoints.Count == 0)
            return;
        var spawnPoint = RootsSpawnPoints[Random.Range(0, RootsSpawnPoints.Count - 1)];
        Instantiate(Roots, spawnPoint.position, Quaternion.identity);
        RootsSpawnPoints.Remove(spawnPoint);
    }

    public void DealDamage(float damage)
    {
        curHP -= damage;
        isDie();
    }

    void isDie()
    {
        if (curHP <= 0)
        {
            BossPhase = BossPhase.Die;
            DestroyAllEnemy();
            Destroy(gameObject);
        }
    }

    private void DestroyAllEnemy()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
    }
}