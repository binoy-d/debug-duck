using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinaleManager : MonoBehaviour
{

    [SerializeField]
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        startFinale(); 
        //Invoke("beep", 3f); 
    }

    public void startFinale()
    {
        gameController.InstantiateSpeechBubble("She doesn’t care about me…"); 

    }
}
