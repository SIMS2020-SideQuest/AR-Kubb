// För att hålla musiken aktiv i alla scener

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class AudioManager : MonoBehaviour 
{
 
    void Awake () //startar så fort spelet startar
    {
    
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if(objs.Length > 1)
            Destroy(this.gameObject); // Räknar hur många music taggar den hittar i scenen och om det är mer än 1 så förstör en de som är fler, vi har denna pga om man går in i spelt och sedan trycker tillbaka så kommer bgm spelar dubbelt då det finns två st.
            
         DontDestroyOnLoad(this.gameObject); // Om det inte är mer än 1 music tag hittat så fortsätter den att utan avbrytan spela våran musik även när scener bytts.
    }
 
       
}
 