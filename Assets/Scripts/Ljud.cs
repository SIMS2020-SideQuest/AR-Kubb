using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 
public class Ljud : MonoBehaviour
{
 
    public GameObject audioOnIcon; // skapae flikarna i Unity som vi kan lägga våra bilder på on/off iconen i
    public GameObject audioOffIcon;

 
    // Use this for initialization
    void Start () 
    {
        SetSoundState (); //kollar funtionen direkt när spelet startas
 
    }
 
   
    public void ToggleSound()
    {
        if (PlayerPrefs.GetInt ("Muted", 0) == 0) //PlayerPrefs sparar inställningarna som spelaren vill ha på sitt spel. Om det aldrig har gjort något val gällande musiken så kommer den automatsikt börja på 0 vilket är inte muted.
        {
            PlayerPrefs.SetInt ("Muted", 1); // om den är på 0 om man klickar på knappen går den till 1 tvärtom
        }
        else
        {
 
            PlayerPrefs.SetInt ("Muted", 0);
        }
 
        SetSoundState(); //kallar på funktionen efter varje klick på knappen
    }
 
    private void SetSoundState() // mute off/on
    {
        if (PlayerPrefs.GetInt ("Muted", 0) == 0) //spelet börjar alltid första gången med musiken spelandes.
        {
     
            AudioListener.volume = 1;  // innebär ljudet är på
            audioOnIcon.SetActive (true);  // visar on bilden
            audioOffIcon.SetActive (false); // Gömmeroff bilden
        } 
                else
        {
     
            AudioListener.volume = 0;  // innebär ljud är av
            audioOnIcon.SetActive (false); // Gömmer on bilden
            audioOffIcon.SetActive (true); // visar off bilden
        }
    }
}
 