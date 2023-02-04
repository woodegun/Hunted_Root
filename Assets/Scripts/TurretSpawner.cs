using System.Collections.Generic;
using UnityEngine;

public class TurretSpawner : MonoBehaviour
{
    public static TurretSpawner INSTANT;

    [SerializeField] private GameObject Turret;
    [SerializeField] private List<GameObject> SpawnPoints;

    private int _lastIndex;

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

        var point = SpawnPoints[GetIndex()];
        Instantiate(Turret, point.transform.position, point.transform.rotation);
    }

    private int GetIndex()
    {
        if (SpawnPoints.Count == 1) return 0;
        int result;
        do
        {
            result = Random.Range(0, SpawnPoints.Count - 1);
        } while (result.Equals(_lastIndex));

        _lastIndex = result;
        Debug.Log("Retur index:" + result);
        return result;
    }
}