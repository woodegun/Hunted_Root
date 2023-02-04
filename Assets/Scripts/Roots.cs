using UnityEngine;

public class Roots : MonoBehaviour
{
    [SerializeField] private GameObject RootWeapon;
    private bool createRoots;
    [SerializeField] private float createRootsMaxTimer;
    private float createRootsCurTimer;

    private void Start()
    {
        createRootsMaxTimer = GlobalSettings.INSTANSE.CreateRootsMaxTimer;
        createRootsCurTimer = createRootsMaxTimer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            createRoots = true;
        }
    }

    private void FixedUpdate()
    {
        if (createRoots)
        {
            createRootsCurTimer -= Time.deltaTime;
            if (createRootsCurTimer <= 0)
            {
                Instantiate(RootWeapon, transform);
                createRoots = false;
                createRootsCurTimer = createRootsMaxTimer;
            }
        }
    }
}