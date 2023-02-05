using System.Collections;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject _canvas;
    public bool textTyping;

    public void ShowText(string text)
    {
        _canvas.SetActive(true);
        StartCoroutine(WholeTextCoroutine(text));
    }
    public bool PrintTutorialText(string text)
    {
        if (textTyping)
        {
            return false;
        }
        _textMeshPro.text = "";
        _canvas.SetActive(true);
        StartCoroutine(TextCoroutine(text));
        return true;
    }

    IEnumerator TextCoroutine(string text)
    {
        textTyping = true;
        foreach (char abc in text)
        {
            _textMeshPro.text += abc;
            yield return new WaitForSeconds(0.07f);
        }
        yield return new WaitForSeconds(2f);
        textTyping = false;
        _canvas.SetActive(false);
    }
    
    IEnumerator WholeTextCoroutine(string text)
    {
        _textMeshPro.text = text;
        yield return new WaitForSeconds(3f);
        _canvas.SetActive(false);
    }
}