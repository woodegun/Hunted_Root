using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryController : MonoBehaviour
{
    [SerializeField] private GameObject panel1;
    [SerializeField] private GameObject image1;
    [SerializeField] private StoryTextController text1;
    [SerializeField] private GameObject image2;
    [SerializeField] private StoryTextController text2;

    [SerializeField] private GameObject panel2;
    [SerializeField] private GameObject image3;
    [SerializeField] private StoryTextController text3;
    [SerializeField] private GameObject image4;
    [SerializeField] private StoryTextController text4;
    private void Start()
    {
        panel1.SetActive(false);
        image1.SetActive(false);
        text1.gameObject.SetActive(false);
        image2.SetActive(false);
        text2.gameObject.SetActive(false);
        panel2.SetActive(false);
        image3.SetActive(false);
        text3.gameObject.SetActive(false);
        image4.SetActive(false);
        text4.gameObject.SetActive(false);
    }

    private void Skip()
    {
        SceneManager.LoadScene(1);
    }

    private void FixedUpdate()
    {
        if (Input.anyKey)
        {
            Skip();
        }

        if (!text1.isFinish && !text1.isStarted)
        {
            panel1.SetActive(true);
            PrintPart(image1, text1);
        }

        if (text1.isFinish && !text2.isStarted)
        {
            PrintPart(image2, text2);
            return;
        }
        
        if (text2.isFinish && !text3.isStarted)
        {
            panel2.SetActive(true);
            panel1.SetActive(false);
            PrintPart(image3, text3);
            return;
        }
        
        if (text3.isFinish && !text4.isStarted)
        {
            PrintPart(image4, text4);
            return;
        }
    }

    private void PrintPart(GameObject image, StoryTextController text)
    {
        image.SetActive(true);
        text.gameObject.SetActive(true);
        text.PrintText();
    }
}