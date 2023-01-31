using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] PatrolPoints;
    private Transform _nextPatrolPoint;
    private int _nextPatrolPointIndex;

    private NavMeshAgent _navMeshAgent;
    private Transform _player;
    private Transform _target;
    private bool _moveToTarget;

    private const float VisibilityRangeMin = 30f;
    private const float AdditionalVisibilityRange = 10f;
    private float _currentVisibilityRange = 30f;
    private float _hearingRange = 90f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _nextPatrolPoint = PatrolPoints[_nextPatrolPointIndex];
        _target = _nextPatrolPoint;
    }

    private void FixedUpdate()
    {
        HuntPlayer();
        Patrol();
        if (_target != null)
        {
            _navMeshAgent.destination = _target.position;
        }
    }

    private void HuntPlayer()
    {
        float distanceToPlayer = GetDistanceTo(_player);
        if (distanceToPlayer <= _currentVisibilityRange && _target != _player)
        {
            CapturePlayer();
        }

        if (distanceToPlayer > _currentVisibilityRange && _target == _player)
        {
            LetPlayerGo();
        }
    }

    private void Patrol()
    {
        if (_target == _player)
        {
            return;
        }

        float distanceToPatrolPoint = GetDistanceTo(_nextPatrolPoint);
        if (distanceToPatrolPoint <= 1)
        {
            UpdateNextPatrolPoint();
            _target = _nextPatrolPoint;
        }
    }

    private void CapturePlayer()
    {
        _currentVisibilityRange = VisibilityRangeMin + AdditionalVisibilityRange;
        _target = _player;
    }

    private void LetPlayerGo()
    {
        _currentVisibilityRange = VisibilityRangeMin;
        _target = _nextPatrolPoint;
    }

    private float GetDistanceTo(Transform _target)
    {
        var heading = _target.position - transform.position;
        var distance = heading.magnitude;
        return distance;
    }

    private void UpdateNextPatrolPoint()
    {
        _nextPatrolPointIndex++;
        if (PatrolPoints.Length <= _nextPatrolPointIndex)
        {
            _nextPatrolPointIndex = 0;
        }

        _nextPatrolPoint = PatrolPoints[_nextPatrolPointIndex];
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _currentVisibilityRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _hearingRange);
    }
}