using System.Collections;
using TMPro;
using UnityEngine;

public class StoryTextController : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;
    public bool isFinish;
    public bool isStarted = false;


    private void Awake()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    public void PrintText()
    {
        isStarted = true;
        var text = _textMeshPro.text;
        _textMeshPro.text = "";
        StartCoroutine(TextCoroutine(text));
    }

    IEnumerator TextCoroutine(string text)
    {
        isFinish = false;
        foreach (char abc in text)
        {
            _textMeshPro.text += abc;
            yield return new WaitForSeconds(0.07f);
        }

        yield return new WaitForSeconds(2f);
        isFinish = true;
    }
}