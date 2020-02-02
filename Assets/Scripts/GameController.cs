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
    [SerializeField] private GameObject JukeBox = null;

    private List<string> allLines = new List<string>();

    private bool intro_done = false;

    private int programmer_health = 6;

    private LineParser lp;
    [SerializeField] float shoot_delay = 0.8f;
    public bool playGameSequence = true;

    [SerializeField] GameObject credits;

    private float waitTime = 0f;

    [SerializeField] GameObject programmer_sprite = null;
    [SerializeField] GameObject paused = null;

    int number_of_lines;
    
    public static GameController instance = null;

    void Start()

    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad((gameObject));
        }
        else
        {
            Destroy(gameObject);
        }
        lp = GetComponent<LineParser>();
        number_of_lines = 0;
        LoadAllText("Assets/lines.txt");

        if(playGameSequence)
            StartCoroutine(Intro());
    }
    public int getHealth(){
        return programmer_health;
    }
    public void LoadAllText(string file_name)
    {
        /*StreamReader reader = new StreamReader(file_name);

        string line = reader.ReadLine();
        do
        {
            allLines.Add(line);
            line = reader.ReadLine();
            number_of_lines++;
        } while (line != null);
        reader.Close();*/

        string t = @"4-Hello uhm… Rubber Duck!
0 + She’s mocking me.What kinda anniversary gift is a rubber duck ? / She just wants me to do better… by practicing Rubber Duck Debugging.
3 - Nice, I got shooting to work - first try.
5 - But I’ve been having trouble getting it to move up and down.
  5 - I guess this is where you come in, huh Ducky? 
3 - So my code goes… 
0 +if (verticalPressed) position.y + moveAmount; / if (verticalPressed) position.y += moveAmount
  3 - Okay, now they can move with the up - down keys.
  3 - But I can’t believe I missed that.
  2 - This is like the simplest thing. 
3 - (And without you Ducky, I probably would’ve taken twice as long…) 
3 - Okay deep breaths. Let’s see oh my, the shooting is broken
  3 - That fire rate is CLEARLY overpowered. 
2 - Let’s see, how can we cripple the player ?
  4 + void update() { reloadCooldown -= Time.time; } / void update() { reloadCooldown -= Time.deltaTime }
        m,vb
  3 - Don’t worry, being a mess is endearing.
  4 - Or so I’ve been told.
  2 - Okay, Duck, time to shoot for the stars. Can we -
    4 - One second I gotta take this call.
    2 - *Yeah of course, don’t worry about it.*
    2 - *No no no, don’t apologize.*
    3 - *It’s totally fine.We can play Smash some other time.*
    3 - *Oh he’s gonna teach you ? That’s so cool.I hope you two have lots of fun.*
    4 - *Yeah, I wouldn’t pass up the opportunity to play with a Nintendo Developer either haha.*
    1 - *ahem *
    3 - *Okay, where were we ?
    3 - Oh yeah, debugging the horizontal movement!
    1 - The problem’s probably here: 
2 +if (false && horzontal) position.x += moveAmount; if (horizontalPressed) position.x += moveAmount / if (horizontalPressed) position.x += moveAmount; if (horizontalPressed) position.x += moveAmount
  4 + I can’t compete with him. As a smash player and a partner. / They’re just friends.There’s a reason she’s with me.
  5 + int numMovementAxis = 1; / int numMovementAxis = 2;
        2 + Did he skip work to be with her? He’s gotta have ulterior motives. / He probably just wants to unwind after crunch season.
3 + dettectMoreKeyes(); / detectMoreKeys();
        6 + What does she even see in me? I talk to a Rubber Duck.He talks to Masahiro Sakurai. / We tend to be our own worst critic.I’ve gotta be nicer to myself. 
3 - Whew.That took a while.
 3 - But we did it Ducky!We’re on our way!
  2 - Let’s keep it up. Next bug: Sprite Swapping!
    2 - Let’s see, it goes
  6 + void OnCatharsis() { spriteRenderer.sprite == trueForm; } / void OnCatharsis() { spriteRenderer.sprite = trueForm; }
        4 - Oh boy, looks like that’s not the only thing wrong. 
4 - There’s so many bugs everywhere.
  1 + If(vulnerabilityAttitude == attitudes.ccomfoseat) { OpenUp(); } / If(vulnerabilityAttitude == attitudes.comfortable) { OpenUp(); }
        1 + void Update() { honesty +; } / void Update() { honesty++; }
        6 +private insecurity[] insecurities; / public insecurity[] insecurities;
4-Okay, all those typoes are fixed
4 - But for some reason it’s still crashing ?
1 +while(notGoodEnough) { struggle();
} / while(notGoodEnough) { break; struggle(); }
1+while(notGoodEnough) { struggle(); } / while(notGoodEnough) { break; struggle(); }
2+while(notGoodEnough) { struggle(); } / while(notGoodEnough) { break; struggle(); }
4-Argh, why doesn’t this work?? 
4-I’m gonna take a break and look at my phone, Duck.
5-Forgive me.  
3-Wait...
4-“Thanks to my best friend for the Gold Developer-Edition Nintendo Switch?” 
4-“I can finally get rid of my crappy Switch Lite lol”?? 
2-I got her that Switch Lite… 
9+She doesn’t care about me… / She doesn’t love me. / She’s never loved me. / She’s gonna leave me for me him… / She’s gonna break up with me / She’s gonna cheat on me it’s only a matter of time / I can do this. I have to leave her before she cheats on me. / I’ve gotta cheat on her before she cheats on me. / Okay, okay duck I know that’s ridiculous. I know I’ve just gotta grow up and accept I’m inferior. / I know I’m messed up. / I know I’ve got problems. / I know… / I know I need help.
4-Hey, listen.
4-Can we talk?";
        string[] l = t.Split('\n');
        foreach (string s in l)
        {
            allLines.Add(s);
            number_of_lines++;
        }
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
        GameObject fb = null;
        float tt = 0f;
        for (int i = start; i < end-2; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "" && i != end - 3)
            {
                float time = InstantiateSpeechBubble(t);
                yield return new WaitForSeconds(time);
            }
            else if (i == end-3)
            {
                fb = GameObject.Instantiate(FINAL_BUBBLE);
                fb.GetComponent<FinalBubble>().SetInteractable(true);
                fb.GetComponent<FinalBubble>().SetText(t.Substring(2));
                tt = lp.TimeBeforeLine(t);
            }
        }
        
        JukeBox.GetComponent<JukeBox>().Pause();
        JukeBox.GetComponent<JukeBox>().PlayOminous();
        yield return new WaitForSeconds(tt);
        
        while(fb != null && !fb.GetComponent<FinalBubble>().GetHit())
        {
            yield return new WaitForSeconds(0.1f);
        }

        for (int i = end-2; i < end; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                float time = InstantiateSpeechBubble(t);
                yield return new WaitForSeconds(time);
            }
        }
        
        JukeBox.GetComponent<JukeBox>().Pause();
        JukeBox.GetComponent<JukeBox>().PlayUplifting();
        credits.SetActive(true);

        yield return null;
    }


    private IEnumerator Intro()
    {
        JukeBox.GetComponent<JukeBox>().Pause();
        JukeBox.GetComponent<JukeBox>().PlayArcade();
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

        for (int i = 2; i < 7; i++)
        {
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                sb = InstantiateSpeechBubbleGO(t);
                yield return new WaitForSeconds(waitTime);
            }
        }
        while (sb != null && !sb.GetComponent<SpeechBubble>().GetHit())
        {
            sb.GetComponent<SpeechBubble>().SetY(0f);
            yield return new WaitForSeconds(0.5f);
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
        while (sb != null && !sb.GetComponent<SpeechBubble>().GetHit())
        {
            yield return new WaitForSeconds(0.5f);
        }
        GameObject.Find("debug_duck_64").GetComponent<Controller>().SetShootDelay(shoot_delay);

        for (int i = 14; i < 28; i++)
        {
            if (i == 18)
            {
                while (programmer_sprite.transform.position.x < 16f)
                {
                    programmer_sprite.transform.position = new Vector2(programmer_sprite.transform.position.x + Time.deltaTime * 50f,
                        programmer_sprite.transform.position.y);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else if (i == 23)
            {
                while (programmer_sprite.transform.position.x > 9.3f)
                {
                    programmer_sprite.transform.position = new Vector2(programmer_sprite.transform.position.x - Time.deltaTime * 50f,
                        programmer_sprite.transform.position.y);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            string t = lp.ParseLine(allLines[i]);
            if (t != "")
            {
                sb = InstantiateSpeechBubbleGO(t);
                yield return new WaitForSeconds(waitTime);
            }
        }
        while (sb != null && !sb.GetComponent<SpeechBubble>().GetHit())
        {
            yield return new WaitForSeconds(0.5f);
        }
        GameObject.Find("debug_duck_64").GetComponent<Controller>().SetCanMove(true, true);
        

        StartCoroutine(MainGame(28, number_of_lines));

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
            programmer_health = 6;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            StartCoroutine(Intro());
        }

        //DEBUG CONTROLS

        if (Input.GetKeyDown(KeyCode.RightBracket))
        {
            float f = Time.timeScale * 2;
            Time.timeScale = f >= 100f ? 100f : f;
        }
        if (Input.GetKeyDown(KeyCode.LeftBracket))
            Time.timeScale *= 0.5f;
    }

    public void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0f ? 1f : 0f;
        paused.SetActive(Time.timeScale == 0f);
    }
}
