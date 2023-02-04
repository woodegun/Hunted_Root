using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretSpawner : MonoBehaviour
{
    public static TurretSpawner INSTANT;

    [SerializeField] private GameObject Turret;
    [SerializeField] private List<GameObject> SpawnPoints;

    private void Awake()
    {
        INSTANT = this;
    }

    private void Start()
    {
        SpawnNext();
    }

    public void SpawnNext()
    {
        var point = SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];
        Instantiate(Turret, point.transform.position, point.transform.rotation);
    }
}