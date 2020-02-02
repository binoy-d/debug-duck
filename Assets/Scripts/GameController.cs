using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject SPEECH_BUBBLE = null;

    private List<string> allLines = new List<string>();

    private bool intro_done = false;

    void Start()
    {
        LoadAllText("lines.txt");

        //StartCoroutine(Wave(0, allLines.Count, 5f));
        //Intro sequence
        //StartCoroutine(Wave(0, 5, 6f));
        StartCoroutine(Intro());
    }
    
    void Update()
    {
        
    }

    private void LoadAllText(string file_name)
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

    private GameObject InstantiateSpeechBubble(string s)
    {
        GameObject sb = GameObject.Instantiate(SPEECH_BUBBLE);
        sb.GetComponent<SpeechBubble>().SetInteractable(LineParser.instance.LineIsInteractable(s));
        sb.GetComponent<SpeechBubble>().SetText(s.Substring(1));
        return sb;
    }

    private IEnumerator Wave(int start, int end, float time_btwn)
    {
        for (int i = start; i <= end; i++)
        {
            string t = LineParser.instance.ParseLine(allLines[i]);
            if (t != "")
            {
                InstantiateSpeechBubble(t);
                yield return new WaitForSeconds(time_btwn);
            }
        }

        yield return null;
    }

    private IEnumerator Intro()
    {
        GameObject sb = null;
        for (int i = 0; i < 5; i++)
        {
            string t = LineParser.instance.ParseLine(allLines[i]);
            if (t != "")
            {
                yield return new WaitForSeconds(3f);
                sb = InstantiateSpeechBubble(t);
            }
        }
        sb.GetComponent<SpeechBubble>().SetCanMove(false);
        while (!intro_done)
        {
            yield return new WaitForSeconds(0.5f);
            intro_done = sb == null;
        }

        StartCoroutine(Wave(5, 10, 2f));

        yield return null;
    }
}
