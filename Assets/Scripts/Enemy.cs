using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform[] PatrolPoints;
    private Transform _nextPatrolPoint;
    private int _nextPatrolPointIndex;

    private NavMeshAgent _navMeshAgent;
    private Transform _player;
    private PlayerController _playerController;
    private Transform _target;
    private Transform _noise;
    private bool _moveToTarget;

    private const float VisibilityRangeMin = 30f;
    private const float AdditionalVisibilityRange = 10f;
    private float _currentVisibilityRange = 30f;
    private float _hearingRange = 100f;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _playerController = _player.GetComponent<PlayerController>();
        _nextPatrolPoint = PatrolPoints[_nextPatrolPointIndex];
        _target = _nextPatrolPoint;
    }

    private void OnEnable()
    {
        Stick.onBranchCrunches += SetNoise;
    }

    private void OnDisable()
    {
        Stick.onBranchCrunches -= SetNoise;
    }

    private void FixedUpdate()
    {
        HuntPlayer();
        HuntNoise();
        Patrol();
        if (_target != null)
        {
            _navMeshAgent.destination = _target.position;
        }
    }

    private void HuntPlayer()
    {
        float distanceToPlayer = GetDistanceTo(_player);
        if (_playerController._playerState != PlayerState.UnderTheGround && distanceToPlayer <= _currentVisibilityRange && _target != _player)
        {
            CapturePlayer();
        }

        if (_playerController._playerState == PlayerState.UnderTheGround || distanceToPlayer > _currentVisibilityRange && _target == _player)
        {
            LetPlayerGo();
        }
    }
    
    private void HuntNoise()
    {
        if (_target == _player || _noise == null)
        {
            return;
        }
        _target = _noise;
        float distanceToNoise = GetDistanceTo(_noise);
        if (distanceToNoise <= 1)
        {
            _noise = null;
            _target = _nextPatrolPoint;
        }
    }

    private void Patrol()
    {
        if (_target == _player || _target == _noise)
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

    private void SetNoise(Transform _noise)
    {
        float distanceToNoise = GetDistanceTo(_noise);
        if (distanceToNoise <= _hearingRange)
        {
            this._noise = _noise;
        }
    }
    
    
}