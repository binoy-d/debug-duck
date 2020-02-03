using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeBox : MonoBehaviour
{
    [SerializeField]
    private AudioSource _arcade;
    [SerializeField]
    private AudioSource _ominous;
    [SerializeField]
    private AudioSource _uplifting;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void PlayArcade()
    {
        _arcade.Play();
    }
    public void PlayOminous()
    {
        _ominous.Play();
    }
    public void PlayUplifting()
    {
        _uplifting.Play();
    }

    public void Pause()
    {
        _arcade.Pause();
        _ominous.Pause();
        _uplifting.Pause();
    }
    
}
