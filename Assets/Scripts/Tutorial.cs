using System;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private TextController _textController;

    private String line1 = "Use the WASD or arrow keys to move";
    private bool isLesson1Completed;
    private bool isLesson2Completed;
    private bool isLesson3Completed;
    private String line2 = "Use your mouse to look around";
    private bool isLesson4Completed;
    private String line3 = "To speed up, hold Shift and move";
    private String line4 = "Press Space to hide";
    private bool isLesson5Completed;
    private bool isLesson6Completed;
    
    private void Start()
    {
        _textController = GetComponent<TextController>();
    }

    private void FixedUpdate()
    {
        if (!isLesson1Completed)
        {
            isLesson1Completed = _textController.PrintTutorialText(line1);
            return;
        }

        if (!isLesson2Completed && (Input.GetAxisRaw("Horizontal")>0 || Input.GetAxisRaw("Vertical")>0))
        {
            isLesson2Completed = true;
        }

        if (isLesson2Completed && !isLesson3Completed)
        {
            isLesson3Completed = _textController.PrintTutorialText(line2);
            return;
        }

        if (isLesson3Completed && !isLesson4Completed)
        {
            isLesson4Completed = _textController.PrintTutorialText(line3);
            return;
        }

        if (isLesson4Completed && !isLesson5Completed && Input.GetKey(KeyCode.LeftShift))
        {
            isLesson5Completed = true;
        }
        
        if (isLesson5Completed && !isLesson6Completed)
        {
            isLesson6Completed = _textController.PrintTutorialText(line4);
            return;
        }
    }
}