using Cinemachine;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public PlayerController player;
    public bool turretUnderControl;

    private CinemachineFreeLook cameraController;

    public GameObject ui;

    private void Start()
    {
        cameraController = FindObjectOfType<CinemachineFreeLook>();
    }

    private void Update()
    {
        if (turretUnderControl)
        {
            transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var obj = other.GetComponent<PlayerController>();
        if (obj != null)
        {
            player = obj;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            player = null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (player != null && player._playerState == PlayerState.UnderTheGround && !turretUnderControl)
        {
            Debug.Log("turretUnderControl");
            if (ui != null)
            {
                ui.SetActive(true);
            }

            turretUnderControl = true;
            cameraController.Follow = transform;
            cameraController.LookAt = transform;
        }

        if (player != null && player._playerState == PlayerState.OnTheGround && turretUnderControl)
        {
            DoDigOut();
        }
    }

    public void DoDigOut()
    {
        if (ui != null)
        {
            ui.SetActive(false);
        }

        Debug.Log("!turretUnderControl");
        turretUnderControl = false;
        cameraController.Follow = player.transform;
        cameraController.LookAt = player.transform;
        player.DoDigOut();
    }
}