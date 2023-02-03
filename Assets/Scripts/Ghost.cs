using UnityEngine;

public class Ghost : EnemyBehaviour
{
    [SerializeField] private float Speed;
    
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

    public override void DealDamage(int damage)
    {
        Debug.Log("Мне пофиг");
    }

    public override void Scare()
    {
        Destroy(gameObject);
    }
}