using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Management;

namespace UnityEngine.XR.ARFoundation.ARKubb{
    public class ARSceneSelectUI : MonoBehaviour{
       
        [SerializeField]
        Scrollbar m_HorizontalScrollBar;
        public Scrollbar horizontalScrollBar{
            get => m_HorizontalScrollBar;
            set => m_HorizontalScrollBar = value;
        }

        [SerializeField]
        Scrollbar m_VerticalScrollBar;
        public Scrollbar verticalScrollBar{
            get => m_VerticalScrollBar;
            set => m_VerticalScrollBar = value;
        }

        [SerializeField]
        GameObject m_MainMenu;
        public GameObject mainmenu{
            get { return m_MainMenu; }
            set { m_MainMenu = value; }
        }

        void Start(){
            ScrollToStartPosition();
        }

        static void LoadScene(string sceneName){
            LoaderUtility.Initialize();
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }

        public void PlayButtonPressed(){
            LoadScene("Game");
        }
       
        public void HelpButtonPressed(){
            LoadScene("Help");
        }

        public void SoundButtonPressed(){
            LoadScene("Sound");
        }

        public void BackButtonPressed(){
            ActiveMenu.currentMenu = MenuType.Menu;
            m_MainMenu.SetActive(true);
            ScrollToStartPosition();
        }

        void ScrollToStartPosition(){
            m_HorizontalScrollBar.value = 0;
            m_VerticalScrollBar.value = 1;
        }
    }
}
