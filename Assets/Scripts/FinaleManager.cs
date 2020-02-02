using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleManager : MonoBehaviour
{

    [SerializeField]
    GameController gameController;
    string rawText = "She doesn’t care about me… / She doesn’t love me. / She’s never loved me. / She’s gonna leave me for me him… / She’s gonna break up with me / She’s gonna cheat on me it’s only a matter of time / I can do this. I have to leave her before she cheats on me. / I’ve gotta cheat on her before she cheats on me. / Okay, okay duck I know that’s ridiculous. I know I’ve just gotta grow up and accept I’m inferior. / I know I’m messed up. / I know I’ve got problems. / I know… / I know I need help.";
    string[] lines = rawText.Split("/");
    string  currentLine = lines[0];
    string index = 0;
    // Start is called before the first frame update
    void Start()
    {
        startFinale(); 

    }

    public void startFinale()
    {
        gameController.InstantiateSpeechBubble(rawText); 

    }
    public string getNextLine(){
        index++;
        return lines[index];
    }

}
