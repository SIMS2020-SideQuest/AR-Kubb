using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void playGame() //funktion för playbutton
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Plussar på 1 så att nästa scene loadas 
    }
}
