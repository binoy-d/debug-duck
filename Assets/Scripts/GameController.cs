using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject SPEECH_BUBBLE = null;
    [SerializeField] private GameObject FINAL_BUBBLE = null;

    private List<string> allLines = new List<string>();

    private bool intro_done = false;

    private int programmer_health = 6;

    private LineParser lp;
    [SerializeField] float shoot_delay = 0.8f;
    public bool playGameSequence = true;

    [SerializeField] GameObject credits;

    private float waitTime = 0f;

    [SerializeField] GameObject programmer_sprite = null;

    void Awake()

    {
        lp = GetComponent<LineParser>();
        LoadAllText("lines.txt");

        if(playGameSequence)
            StartCoroutine(Intro());
    }
    public int getHealth(){
        return programmer_health;
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
        waitTime = lp.TimeBeforeLine(s);
        return sb;
    }

    public float InstantiateSpeechBubble(string s)
    {
        GameObject sb = GameObject.Instantiate(SPEECH_BUBBLE);
        sb.GetComponent<SpeechBubble>().SetInteractable(lp.LineIsInteractable(s));
        sb.GetComponent<SpeechBubble>().SetText(s.Substring(2));
        return lp.TimeBeforeLine(s);
    }

    private IEnumerator MainGame(int start, int end)
    {
        for (int i = start; i < end; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "" && i != end - 3)
            {
                float time = InstantiateSpeechBubble(t);
                yield return new WaitForSeconds(time);
            }
        }
        while (programmer_sprite.transform.position.x < 16f)
        {
            programmer_sprite.transform.position = new Vector2(programmer_sprite.transform.position.x + Time.deltaTime * 25f,
                programmer_sprite.transform.position.y);
            yield return new WaitForSeconds(0.1f);
        }

        //Instantiate(credits, GameObject.Find("Canvas").transform);
        credits.SetActive(true);

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
                sb = InstantiateSpeechBubbleGO(t);
                yield return new WaitForSeconds(waitTime);
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
                sb = InstantiateSpeechBubbleGO(t);
                yield return new WaitForSeconds(waitTime);
            }
        }
        sb.GetComponent<SpeechBubble>().SetCanMove(false);
        while (!intro_done)
        {
            sb.GetComponent<SpeechBubble>().SetY(0f);
            yield return new WaitForSeconds(0.5f);
            intro_done = sb == null;
        }
        GameObject.Find("debug_duck_64").GetComponent<Controller>().SetCanMove(false, true);

        for (int i = 7; i < 14; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                sb = InstantiateSpeechBubbleGO(t);
                yield return new WaitForSeconds(waitTime);
            }
        }
        while (sb != null)
        {
            yield return new WaitForSeconds(0.5f);
        }
        GameObject.Find("debug_duck_64").GetComponent<Controller>().SetShootDelay(shoot_delay);

        for (int i = 14; i < 28; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                sb = InstantiateSpeechBubbleGO(t);
                yield return new WaitForSeconds(waitTime);
            }
        }
        while (sb != null)
        {
            yield return new WaitForSeconds(0.5f);
        }
        GameObject.Find("debug_duck_64").GetComponent<Controller>().SetCanMove(true, true);

        StartCoroutine(MainGame(28, 58));

        yield return null;
    }

    public void UpdateHealth()
    {
        programmer_health--;
    }

    private void Update()
    {
        if (programmer_health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        //DEBUG CONTROLS
        if (Input.GetKeyDown(KeyCode.RightBracket))
            Time.timeScale *= 2;
        if (Input.GetKeyDown(KeyCode.LeftBracket))
            Time.timeScale *= 0.5f;
    }
}
