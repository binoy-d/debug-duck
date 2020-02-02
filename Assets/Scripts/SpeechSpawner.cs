using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSpawner : MonoBehaviour
{
    public float delay;
    public GameController gc; 
    [TextArea]
    public string credits;
    public string[] lines; 
    private void Start()
    {
        lines = credits.Split('\n');
        Invoke("startDisplayingCredits", delay); 


    }

    void startDisplayingCredits()
    {
        StartCoroutine(lineSpawner());
    }
    IEnumerator lineSpawner()
    {
        foreach(string line in lines)
        {
            gc.InstantiateSpeechBubble(delay +" " + line);
            yield return new WaitForSeconds(delay);  
        }
        
    }
}

