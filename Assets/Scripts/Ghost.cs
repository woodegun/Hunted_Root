using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] private Transform[] PatrolPoints;
    private Transform _nextPatrolPoint;
    private int _nextPatrolPointIndex;
    
    private const float VisibilityRangeMin = 30f;
    private const float AdditionalVisibilityRange = 10f;
    private float _currentVisibilityRange = 30f;
    
    private Transform _player;
    private PlayerController _playerController;
    
    private Transform _target;
    [SerializeField] private float Speed;
    
    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        _playerController = _player.GetComponent<PlayerController>();
        _nextPatrolPoint = PatrolPoints[_nextPatrolPointIndex];
        _target = _nextPatrolPoint;
    }
    
    private void FixedUpdate()
    {
        HuntPlayer();
        Patrol();
        if (_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, Time.deltaTime * Speed);
            transform.LookAt(_target);
        }
    }
    
    private void Patrol()
    {
        if (_target == _player)
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
    
    private void HuntPlayer()
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
        _nextPatrolPointIndex++;
        if (PatrolPoints.Length <= _nextPatrolPointIndex)
        {
            _nextPatrolPointIndex = 0;
        }

        _nextPatrolPoint = PatrolPoints[_nextPatrolPointIndex];
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _currentVisibilityRange);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Die();
        }
    }
    
    public void Scare()
    {
        Destroy(this);
    }
}