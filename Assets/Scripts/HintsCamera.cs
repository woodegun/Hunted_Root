using System;
using UnityEngine;

public class HintsCamera : MonoBehaviour
{
    public Transform mLookAt;

    private void Start()
    {
        mLookAt = Camera.main.transform;
    }

    private void Update()
    {
        if (mLookAt)
        {
            transform.LookAt(2 * transform.position - mLookAt.position);
        }
    }
}