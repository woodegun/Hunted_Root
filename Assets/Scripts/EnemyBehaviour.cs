using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected int HP = 5;

    [SerializeField] protected List<Transform> PatrolPoints;
    protected Transform _nextPatrolPoint;
    protected int _nextPatrolPointIndex;

    protected Transform _player;
    protected PlayerController _playerController;
    protected Transform _target;

    [SerializeField] protected const float VisibilityRangeMin = 30f;
    [SerializeField] protected const float AdditionalVisibilityRange = 10f;
    protected float _currentVisibilityRange = 30f;

    protected void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _playerController = _player.GetComponent<PlayerController>();
        _nextPatrolPoint = transform;
        if (_nextPatrolPoint == null)
        {
            _target = transform;
        }
        else
        {
            _target = _nextPatrolPoint;
        }
    }

    protected void HuntPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);
        if (!_playerController.isElusive() && distanceToPlayer <= _currentVisibilityRange && _target != _player)
        {
            CapturePlayer();
        }

        if (_playerController.isElusive() || (distanceToPlayer > _currentVisibilityRange && _target == _player))
        {
            LetPlayerGo();
        }
    }

    protected void Patrol()
    {
        if (_target != _nextPatrolPoint)
        {
            return;
        }

        float distanceToPatrolPoint = Vector3.Distance(transform.position, _nextPatrolPoint.position);
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

    private void UpdateNextPatrolPoint()
    {
        if (PatrolPoints == null || PatrolPoints.Count == 0)
        {
            _nextPatrolPoint = transform;
            return;
        }

        _nextPatrolPointIndex++;
        if (PatrolPoints.Count <= _nextPatrolPointIndex)
        {
            _nextPatrolPointIndex = 0;
        }

        _nextPatrolPoint = PatrolPoints[_nextPatrolPointIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Die();
        }
    }

    public void DealDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    public abstract void Scare();
}