using UnityEngine;

public class Ghost : EnemyBehaviour
{
    [SerializeField] private float Speed;

    protected void Start()
    {
        base.Start();
        Speed = GlobalSettings.INSTANSE.GhostSpeed;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _currentVisibilityRange);
    }

    public override void Scare()
    {
        Destroy(gameObject);
    }
}