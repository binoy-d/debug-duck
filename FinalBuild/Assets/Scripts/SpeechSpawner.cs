using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSpawner : MonoBehaviour
{
    public float startDelay;
    public GameController gc; 
    [TextArea]
    public string credits;
    public string[] lines; 
    private void Start()
    {
        lines = credits.Split('\n');
        Invoke("startDisplayingCredits", startDelay); 
    }

    void startDisplayingCredits()
    {
        StartCoroutine(lineSpawner());
    }
    IEnumerator lineSpawner()
    {
        foreach(string line in lines)
        {
            yield return new WaitForSeconds(gc.InstantiateSpeechBubble(line));  
        }
        
    }
}

