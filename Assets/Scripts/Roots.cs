using UnityEngine;

public class Roots : MonoBehaviour
{
    [SerializeField] private float createRootsMinTimer;
    [SerializeField] private float createRootsMaxTimer;
    private float createRootsCurTimer;
    [SerializeField] private float rootsStayMinTimer;
    [SerializeField] private float rootsStayMaxTimer;
    private float rootsStayCurTimer;

    private bool isRootStay;

    [SerializeField] private Animator _animator;

    private void Start()
    {
        createRootsMinTimer = GlobalSettings.INSTANSE.CreateRootsMinTimer;
        createRootsMaxTimer = GlobalSettings.INSTANSE.CreateRootsMaxTimer;
        rootsStayMinTimer = GlobalSettings.INSTANSE.RootsStayMinTimer;
        rootsStayMaxTimer = GlobalSettings.INSTANSE.RootsStayMaxTimer;
        createRootsCurTimer = createRootsMaxTimer;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isRootStay)
            return;
        var palyer = other.GetComponent<PlayerController>();
        if (other.GetComponent<PlayerController>() != null)
        {
            palyer.Die();
        }
    }

    private void FixedUpdate()
    {
        if (isRootStay)
        {
            if (rootsStayCurTimer > 0)
            {
                rootsStayCurTimer -= Time.deltaTime;
            }

            if (rootsStayCurTimer <= 0)
            {
                isRootStay = false;
                _animator.SetTrigger("Exit");
                createRootsCurTimer = Random.Range(createRootsMinTimer, createRootsMaxTimer);
            }
        }
        else
        {
            if (createRootsCurTimer > 0)
            {
                createRootsCurTimer -= Time.deltaTime;
            }

            if (createRootsCurTimer <= 0)
            {
                isRootStay = true;
                _animator.SetTrigger("Enter");
                rootsStayCurTimer = Random.Range(rootsStayMinTimer, rootsStayMaxTimer);
            }
        }
    }
}