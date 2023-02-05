using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float Speed = 6;
    private Vector3 SavedPosition;

    void Start()
    {
        _player = FindObjectOfType<PlayerController>().transform;
        SavedPosition = new Vector3(_player.position.x, _player.position.y, _player.position.z);
        Destroy(gameObject, 10);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, SavedPosition, Time.deltaTime * Speed);
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