using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private Image winPanel;
    [SerializeField] private Image winImage;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private TextMeshProUGUI nextBtn;
    private bool win;

    private void Start()
    {
        var color = nextBtn.color;
        color.a = 0;
        nextBtn.color = color;
        color = winPanel.color;
        color.a = 0;
        winPanel.color = color;
        color = winImage.color;
        color.a = 0;
        winImage.color = color;
        winPanel.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (win)
        {
            UpdatePanel();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player._playerState = PlayerState.Win;
            if (winPanel != null)
            {
                Win();
            }
            else
                Debug.LogWarning("Setup winPanel");
        }
    }

    public void Win()
    {
        win = true;
        winPanel.gameObject.SetActive(true);
    }

    private void UpdatePanel()
    {
        if (winPanel == null || nextBtn == null)
        {
            Debug.LogWarning("Заполни nextBtn & winPanel");
            return;
        }

        var color = winImage.color;
        color.a += Time.deltaTime;
        winImage.color = color;
        color = winPanel.color;
        color.a += Time.deltaTime;
        winPanel.color = color;
        if (color.a > 2)
        {
            color = nextBtn.color;
            color.a += Time.deltaTime;
            nextBtn.color = color;
            winText.color = color;
        }
    }
}