using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class BubbleTextSwap : MonoBehaviour
{
    public string rawText;
    string[] texts;
    int currentTextIndex; 
    private void Start()
    {
        currentTextIndex = 0; 
    }

    public void nextText()
    {
        currentTextIndex++; 
       
    }


}
