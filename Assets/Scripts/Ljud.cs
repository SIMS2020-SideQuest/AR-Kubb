using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class Ljud : MonoBehaviour
{
 
    public GameObject audioOnIcon;
    public GameObject audioOffIcon;

 
    // Use this for initialization
    void Start () 
    {
        SetSoundState ();
 
    }
 
    // Update is called once per frame
    void Update () 
    {
        
    }
    
    public void StartGame()
    {

    }
 
    private void ToggleSound()
    {
        if (PlayerPrefs.GetInt ("Muted", 0) == 0) 
        {
            PlayerPrefs.SetInt ("Muted", 1);
        }
        else
        {
 
            PlayerPrefs.SetInt ("Muted", 0);
        }
 
        SetSoundState();
    }
 
    public void SetSoundState()
    {
        if (PlayerPrefs.GetInt ("Muted", 0) == 0)
        {
     
            AudioListener.volume = 1;
            audioOnIcon.SetActive (true);
            audioOffIcon.SetActive (false);
        }
                else
        {
     
            AudioListener.volume = 0;
            audioOnIcon.SetActive (false);
            audioOffIcon.SetActive (true);
        }
    }
}
 