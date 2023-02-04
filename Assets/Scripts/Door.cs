using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject DoorObj;
    [SerializeField] private GameObject Boss;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Boss.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null && DoorObj != null)
        {
            DoorObj.SetActive(true);
            Boss.SetActive(true);
            _audioSource.Play();
        }
    }
}