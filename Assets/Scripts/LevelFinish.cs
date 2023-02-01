using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player._playerState = PlayerState.Win;
            if (winPanel != null)
                winPanel.SetActive(true);
            else
                Debug.LogWarning("Setup winPanel");
        }
    }
}