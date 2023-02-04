using System.Collections.Generic;
using UnityEngine;

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
        if (SpawnPoints == null || SpawnPoints.Count == 0)
        {
            Debug.LogWarning("Заполни SpawnPoints для турелей");
            return;
        }
        var point = SpawnPoints[Random.Range(0, SpawnPoints.Count - 1)];
        Instantiate(Turret, point.transform.position, point.transform.rotation);
    }
}