using UnityEngine;

public class BridgeTurret : MonoBehaviour
{
    public GameObject Bridge;
    public Turret Turret;

    [SerializeField] private float maxScale = 15;
    private float scaleStep = 0.1f;
    private float carScale = 0;


    private void Start()
    {
        Turret = GetComponent<Turret>();
    }

    private void Update()
    {
        if (Turret.turretUnderControl)
        {
            if (Input.GetKey(KeyCode.Mouse0) && carScale<=maxScale)
            {
                carScale += scaleStep;
                Bridge.transform.localScale += new Vector3(0, scaleStep, 0);
                // Bridge.transform.localPosition += new Vector3(0, 0, scaleStep/2);
            }

            if (Input.GetKey(KeyCode.Mouse1) && carScale>=0)
            {
                carScale -= scaleStep;
                Bridge.transform.localScale -= new Vector3(0, scaleStep, 0);
                // Bridge.transform.localPosition -= new Vector3(0, 0, scaleStep/2);
            }
        }
    }
}