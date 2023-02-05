using System;
using UnityEngine;

public class Web : MonoBehaviour
{
    [SerializeField] private float _liveTime;

    private void Start()
    {
        _liveTime = GlobalSettings.INSTANSE.WebLiveTime;
    }

    private void FixedUpdate()
    {
        Withering();
    }

    private void Withering()
    {
        _liveTime -= Time.deltaTime;
        if (_liveTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Попался");
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