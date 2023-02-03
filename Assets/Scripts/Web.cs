using System;
using UnityEngine;

public class Web : MonoBehaviour
{
    private float LiveTime = 1500f;

    private void FixedUpdate()
    {
        Withering();
    }

    private void Withering()
    {
        LiveTime -= Time.deltaTime;
        if (LiveTime <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.DecreaseSpeed();
            return;
        }
        
        var skeleton = other.GetComponent<Skeleton>();
        if (skeleton != null)
        {
            skeleton.DecreaseSpeed();
        }
        
        var spider = other.GetComponent<Spider>();
        if (spider != null)
        {
            spider.IncreaseSpeed();
        }
    }
}