using UnityEngine;
using UnityEngine.AI;

public class Skeleton : EnemyBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _noise;
    private float _hearingRange = 100f;
    
    private bool fear;
    private float maxFearTime = 15f;
    private float carFearTime;
    
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    private void FixedUpdate()
    {
        if (fear)
        {
            BeAfraid();
            return;
        }

        HuntPlayer();
        HuntNoise();
        Patrol();
        if (_target != null)
        {
            _navMeshAgent.destination = _target.position;
        }
    }
    
    private void OnEnable()
    {
        Stick.onBranchCrunches += SetNoise;
    }

    private void OnDisable()
    {
        Stick.onBranchCrunches -= SetNoise;
    }
    
    private void SetNoise(Transform _noise)
    {
        float distanceToNoise = Vector3.Distance(transform.position, _noise.position);
        if (distanceToNoise <= _hearingRange)
        {
            this._noise = _noise;
        }
    }
    
    private void HuntNoise()
    {
        if (_target == _player || _noise == null)
        {
            return;
        }

        _target = _noise;
        float distanceToNoise = Vector3.Distance(transform.position, _noise.position);
        if (distanceToNoise <= 1)
        {
            _noise = null;
            _target = _nextPatrolPoint;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentVisibilityRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _hearingRange);
    }
    
    public override void DealDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public override void Scare()
    {
        fear = true;
        carFearTime = maxFearTime;
        _navMeshAgent.destination = transform.position;
    }

    private void BeAfraid()
    {
        if (carFearTime <= 0)
        {
            carFearTime = 0;
            fear = false;
            return;
        }

        carFearTime -= Time.deltaTime;
    }
}