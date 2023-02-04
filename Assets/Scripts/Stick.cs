using System;
using UnityEngine;

public class Stick : MonoBehaviour
{
    private AudioSource _audioSource;
    public delegate void OnBranchCrunches(Transform transform);

    public static event OnBranchCrunches onBranchCrunches;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            _audioSource.Play();
            onBranchCrunches?.Invoke(transform);
        }
    }
}