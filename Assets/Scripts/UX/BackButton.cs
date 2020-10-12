using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
//UnityEngine.XR.ARFoundation.ARKubb
namespace SIMS.SideQuest.ARKubb{
    public class BackButton : MonoBehaviour{
        [SerializeField]
        GameObject m_BackButton;
        public GameObject backButton{
            get => m_BackButton;
            set => m_BackButton = value;
        }

        //Checks if meanu scene can be loaded
        void Start(){
            if (Application.CanStreamedLevelBeLoaded("Menu"))
                m_BackButton.SetActive(true);
        }

        //Waits for backbutton on phone to be pressed
        void Update(){
            //Current scene.
            Scene currentScene = SceneManager.GetActiveScene ();

            // Name of the current scene.
            string sceneName = currentScene.name;
            if(sceneName != "Menu"){
                if (Input.GetKeyDown(KeyCode.Escape))
                    BackButtonPressed();
            }
            else{
                if(Input.GetKeyDown(KeyCode.Escape)) 
                    Application.Quit(); 
            }
        }

        public void ExitApplication(){
            Application.Quit();
        }

        public void BackButtonPressed(){
            if (Application.CanStreamedLevelBeLoaded("Menu")){
                SceneManager.LoadScene("Menu", LoadSceneMode.Single);   //Load Menu
                LoaderUtility.Deinitialize();   //Deinitializes the currently active XR Loader, if one exists. 
                                                //This destroys all subsystems.
            }
        }
    }
}
