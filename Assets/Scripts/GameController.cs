using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject SPEECH_BUBBLE = null;

    private List<string> allLines = new List<string>();

    private bool intro_done = false;

    private int proggrammer_health = 1;

    private LineParser lp;
    [SerializeField] float TIME_BTWN = 4f;
    public bool playGameSequence = true;  
    void Awake()

    {
        lp = GetComponent<LineParser>();
        LoadAllText("lines.txt");

        //StartCoroutine(Wave(0, allLines.Count, 5f));
        //Intro sequence
        //StartCoroutine(Wave(0, 5, 6f));

        if(playGameSequence)
            StartCoroutine(Intro());
    }

    public void LoadAllText(string file_name)
    {
        StreamReader reader = new StreamReader(file_name);

        string line = reader.ReadLine();
        do
        {
            allLines.Add(line);
            line = reader.ReadLine();
        } while (line != null);
        reader.Close();
    }

    private GameObject InstantiateSpeechBubbleGO(string s)
    {
        GameObject sb = GameObject.Instantiate(SPEECH_BUBBLE);
        sb.GetComponent<SpeechBubble>().SetInteractable(lp.LineIsInteractable(s));
        sb.GetComponent<SpeechBubble>().SetText(s.Substring(2));
        return sb;
    }

    public float InstantiateSpeechBubble(string s)
    {
        GameObject sb = GameObject.Instantiate(SPEECH_BUBBLE);
        sb.GetComponent<SpeechBubble>().SetInteractable(lp.LineIsInteractable(s));
        sb.GetComponent<SpeechBubble>().SetText(s.Substring(2));
        return lp.TimeBeforeLine(s);
    }

    private IEnumerator Wave(int start, int end, float time_btwn)
    {
        for (int i = start; i < end; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                float time = InstantiateSpeechBubble(t);
                yield return new WaitForSeconds(time);
            }
        }

        yield return null;
    }

    private IEnumerator Intro()
    {
        GameObject sb = null;
        for (int i = 0; i < 2; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                yield return new WaitForSeconds(3f);
                sb = InstantiateSpeechBubbleGO(t);
            }
        }
        sb.GetComponent<SpeechBubble>().SetCanMove(false);
        while (!intro_done)
        {
            sb.GetComponent<SpeechBubble>().SetY(0f);
            yield return new WaitForSeconds(0.5f);
            intro_done = sb == null;
        }
        intro_done = false;
        for (int i = 2; i < 7; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                yield return new WaitForSeconds(3f);
                sb = InstantiateSpeechBubbleGO(t);
            }
        }
        sb.GetComponent<SpeechBubble>().SetCanMove(false);
        while (!intro_done)
        {
            sb.GetComponent<SpeechBubble>().SetY(0f);
            yield return new WaitForSeconds(0.5f);
            intro_done = sb == null;
        }

        StartCoroutine(Wave(7, 55, TIME_BTWN));

        yield return null;
    }

    public void UpdateHealth()
    {
        proggrammer_health--;
    }

    private void Update()
    {
        if (proggrammer_health <= 0)
        {
            SceneManager.LoadScene("Testing");
        }
    }
}
