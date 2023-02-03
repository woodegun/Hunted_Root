using UnityEngine;
using UnityEngine.AI;

public class Spider : Skeleton
{
    [SerializeField] private GameObject Web;

    private float curWebShootTime;
    [SerializeField] private float maxWebShootTime;
    [SerializeField] private float NormalSpeed = 5;
    [SerializeField] private float SuperSpeed = 8;
    [SerializeField] private float curSuperSpeedTime;
    [SerializeField] private float maxSuperSpeedTime = 2;

    private bool isSpeedIncreased;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        SpeedUpdate();
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

        ShootWeb();
    }

    private void ShootWeb()
    {
        if (curWebShootTime <= 0)
        {
            Instantiate(Web, transform.position, transform.rotation);
            curWebShootTime = maxWebShootTime;
        }
        else
        {
            curWebShootTime -= Time.deltaTime;
        }
    }

    public void IncreaseSpeed()
    {
        isSpeedIncreased = true;
        curSuperSpeedTime = maxSuperSpeedTime;
    }

    private void SpeedUpdate()
    {
        if (isSpeedIncreased && curSuperSpeedTime > 0)
        {
            _navMeshAgent.speed = SuperSpeed;
            curSuperSpeedTime -= Time.deltaTime;
        }
        else
        {
            isSpeedIncreased = false;
            _navMeshAgent.speed = NormalSpeed;
        }
    }
}