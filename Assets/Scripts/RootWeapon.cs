using UnityEngine;

public class RootWeapon : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 4);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Die();
        }
    }
}