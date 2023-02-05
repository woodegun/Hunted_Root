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
    
    [SerializeField] private GameObject panel3;
    [SerializeField] private GameObject image5;
    [SerializeField] private StoryTextController text5;
    [SerializeField] private GameObject image6;
    [SerializeField] private StoryTextController text6;
    
    [SerializeField] private GameObject panel4;
    [SerializeField] private GameObject image7;
    [SerializeField] private StoryTextController text7;
    [SerializeField] private GameObject image8;
    [SerializeField] private StoryTextController text8;
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
        panel3.SetActive(false);
        image5.SetActive(false);
        text5.gameObject.SetActive(false);
        image6.SetActive(false);
        text6.gameObject.SetActive(false);
        panel4.SetActive(false);
        image7.SetActive(false);
        text7.gameObject.SetActive(false);
        image8.SetActive(false);
        text8.gameObject.SetActive(false);
    }

    private void Skip()
    {
        SceneManager.LoadScene(2);
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
        
        if (text4.isFinish && !text5.isStarted)
        {
            panel3.SetActive(true);
            panel2.SetActive(false);
            PrintPart(image5, text5);
            return;
        }
        
        if (text5.isFinish && !text6.isStarted)
        {
            PrintPart(image6, text6);
            return;
        }
        
        if (text6.isFinish && !text7.isStarted)
        {
            panel4.SetActive(true);
            panel3.SetActive(false);
            PrintPart(image7, text7);
            return;
        }
        
        if (text7.isFinish && !text8.isStarted)
        {
            PrintPart(image8, text8);
            return;
        }

        if (text8.isFinish)
        {
            Invoke("Skip", 10);
        }
    }

    private void PrintPart(GameObject image, StoryTextController text)
    {
        image.SetActive(true);
        text.gameObject.SetActive(true);
        text.PrintText();
    }
}