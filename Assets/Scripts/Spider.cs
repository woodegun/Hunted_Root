using UnityEngine;
using UnityEngine.AI;

public class Spider : Skeleton
{
    [SerializeField] private GameObject Web;

    private float curWebShootTime;
    [SerializeField] private float maxWebShootTime;
    [SerializeField] private float NormalSpeed;
    [SerializeField] private float SuperSpeed;
    [SerializeField] private float maxSuperSpeedTime;
    [SerializeField] private float curSuperSpeedTime;
    
    [SerializeField] private Animator SpiderAnimator;

    private bool isSpeedIncreased;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        base.Start();
        maxWebShootTime = GlobalSettings.INSTANSE.MaxWebShootTime;
        NormalSpeed = GlobalSettings.INSTANSE.SpiderNormalSpeed;
        SuperSpeed = GlobalSettings.INSTANSE.SpiderSuperSpeed;
        maxSuperSpeedTime = GlobalSettings.INSTANSE.SpiderMaxSuperSpeedTime;
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
            SpiderAnimator.SetFloat("Speed", 1f);
        }
        else
        {
            SpiderAnimator.SetFloat("Speed", 0f);
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