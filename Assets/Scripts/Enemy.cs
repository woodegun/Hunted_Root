using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform target;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        if (target != null)
        {
            _navMeshAgent.destination = target.position;
        }
    }
}