using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
public class CreditsPlayer : MonoBehaviour
{
    [SerializeField]
    float fadeTime, bannerPopUpDelay, bannerDisplayTime;
    [SerializeField]
    Image black, banner;  
    public void Start()
    {
        StartCoroutine(creditsCoroutine()); 
    }

    public IEnumerator creditsCoroutine()
    {
        black.GetComponent<Image>().CrossFadeAlpha(0, 0, false);
        black.GetComponent<Image>().CrossFadeAlpha(1, fadeTime, false);
        yield return new WaitForSeconds(fadeTime + bannerPopUpDelay);
        banner.enabled = true;
        yield return new WaitForSeconds(bannerDisplayTime);
        SceneManager.LoadScene("Credits"); 
    }
}
