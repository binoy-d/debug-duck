using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; 
public class Fade : MonoBehaviour
{
    public void FadeIn(float time)
    {
        GetComponent<Image>().CrossFadeAlpha(1, time, false);  
    }
}
