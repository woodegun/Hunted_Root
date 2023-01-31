using UnityEngine;

public class Stick : MonoBehaviour
{
    public delegate void OnBranchCrunches(Transform transform);

    public static event OnBranchCrunches onBranchCrunches;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Хруст");
            if (onBranchCrunches != null) onBranchCrunches(transform);
        }
    }
}