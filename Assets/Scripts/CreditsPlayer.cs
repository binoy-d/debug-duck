using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement; 
public class CreditsPlayer : MonoBehaviour
{
    [SerializeField]
    float fadeTime, bannerFadeDelay, bannerFadeTime,  bannerDisplayTime;
    [SerializeField]
    Image black;
    public TextMeshProUGUI banner;  
    public void Start()
    {
        StartCoroutine(creditsCoroutine()); 
    }

    public IEnumerator creditsCoroutine()
    {
        black.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
        black.GetComponent<Image>().CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(fadeTime + bannerFadeDelay);
        banner.enabled = true;
        banner.CrossFadeAlpha(0, 0, false);
        banner.CrossFadeAlpha(1, bannerFadeTime, false); 
        yield return new WaitForSeconds(bannerDisplayTime);
        SceneManager.LoadScene("Credits"); 
    }
}
