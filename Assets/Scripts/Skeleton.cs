using UnityEngine;
using UnityEngine.AI;

public class Skeleton : EnemyBehaviour
{
    protected NavMeshAgent _navMeshAgent;
    protected Transform _noise;
    protected float _hearingRange = 100f;

    protected bool fear;
    protected float maxFearTime = 15f;
    protected float carFearTime;

    //web
    private bool isSpeedDecreased;
    private float curDecreasedSpeedTime;
    private float maxDecreasedSpeedTime = 2;
    [SerializeField] private float decreasedSpeed = 3;
    [SerializeField] private float normalSpeed = 5;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = normalSpeed;
    }

    private void FixedUpdate()
    {
        if (fear)
        {
            BeAfraid();
            return;
        }

        SpeedUpdate();
        HuntPlayer();
        HuntNoise();
        Patrol();
        if (_target != null)
        {
            _navMeshAgent.destination = _target.position;
        }
    }

    protected void OnEnable()
    {
        Stick.onBranchCrunches += SetNoise;
    }

    protected void OnDisable()
    {
        Stick.onBranchCrunches -= SetNoise;
    }

    protected void SetNoise(Transform _noise)
    {
        float distanceToNoise = Vector3.Distance(transform.position, _noise.position);
        if (distanceToNoise <= _hearingRange)
        {
            this._noise = _noise;
        }
    }

    protected void HuntNoise()
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

    protected void OnDrawGizmosSelected()
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

    protected void BeAfraid()
    {
        if (carFearTime <= 0)
        {
            carFearTime = 0;
            fear = false;
            return;
        }

        carFearTime -= Time.deltaTime;
    }

    public void DecreaseSpeed()
    {
        isSpeedDecreased = true;
        curDecreasedSpeedTime = maxDecreasedSpeedTime;
    }

    private void SpeedUpdate()
    {
        if (isSpeedDecreased)
        {
            curDecreasedSpeedTime -= Time.deltaTime;
            if (curDecreasedSpeedTime <= 0)
            {
                isSpeedDecreased = false;
                _navMeshAgent.speed = normalSpeed;
            }
            else
            {
                _navMeshAgent.speed = decreasedSpeed;
            }
        }
    }
}