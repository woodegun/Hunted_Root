using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly int damage = 1;

    private void Start()
    {
        Destroy(gameObject, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<EnemyBehaviour>();
        if (enemy != null)
        {
            enemy.DealDamage(damage);
        }
        
        var boss = other.GetComponent<Boss>();
        if (boss != null)
        {
            boss.DealDamage(damage);
        }
        Destroy(gameObject);
    }
}