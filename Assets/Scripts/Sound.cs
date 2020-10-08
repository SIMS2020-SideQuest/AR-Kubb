using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [Tooltip("Sound On or Off?")]
    public bool sound = true;
    
    AudioSource _audioSource;
    //AudioListener _audioListener;
    public GameObject AudioManager;
    
    void Start()
    {
        //_audioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        _audioSource = GetComponent<AudioSource>();
        ToggleSound(sound);
    }

    public void DisableAudio()
    {
        ToggleSound(false);
    }
    
    public void EnableAudio()
    {
        ToggleSound(true);
    }
 
    public void Toggle()
    {
        if(sound)
            DisableAudio();
        else
            EnableAudio();
    }
     
    public void ToggleSound(bool sound)
    {
        if (sound){
            _audioSource.volume = 1f;}
        else{
            _audioSource.volume = 0f;}
        sound = this.sound;
    }
}
