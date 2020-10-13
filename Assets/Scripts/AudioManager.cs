// För att hålla musiken aktiv i alla scener

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AudioManager : MonoBehaviour {
 
        public AudioSource BGM;
 
    // Use this for initialization
    void Start () 
    {
             DontDestroyOnLoad (gameObject); // Förklarar att vi ej vill förstöra scriptet när vi går över till en ny scen, vi vill alltså att den ska fortsätta köras.
    }
 
    // Update is called once per frame
    void Update () 
    {
 
      ///   public void ChangeBGM (AudioClip music)
     ///   {
        ///        if (BGM.clip.name == music.name)
           ///             return;
     ///
        ///        BGM.Stop();
           ///     BGM.clip = music;
              ///  BGM.Play();
        }
        
        
    }
 
       
}
 