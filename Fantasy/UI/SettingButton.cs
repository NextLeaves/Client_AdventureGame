/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/


using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Fantasy.UI
{
    public class SettingButton : MonoBehaviour
    {
        public Button setting_btn;
        public GameObject settingPanel;

        [Space]
        public Button info_btn;
        public Button music_btn;
        public Image musicblock_img;
        public Button cmusic_btn;
        public Button quit_btn;

        private AudioSource bg_audio;

        private void Awake()
        {
            bg_audio = GameObject.Find("SceneManagement/bgManagement").GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (settingPanel.activeSelf) settingPanel.SetActive(!settingPanel.activeSelf);
            setting_btn.onClick.AddListener(OpenSettingPanel);


            music_btn.onClick.AddListener(OpenMusicClick);
            cmusic_btn.onClick.AddListener(OpenCMusicClick);
            quit_btn.onClick.AddListener(QuitGame);
        }

        void OpenSettingPanel()
        {
            settingPanel.SetActive(!settingPanel.activeSelf);
        }

        void OpenInfoClick()
        {

            OpenSettingPanel();
        }

        void OpenMusicClick()
        {
            if (bg_audio.isPlaying)
            {
                bg_audio.Pause();
                musicblock_img.enabled = true;
            }
            else
            {
                bg_audio.Play();
                musicblock_img.enabled = false;
            }

            OpenSettingPanel();
        }

        void OpenCMusicClick()
        {
            
        }

        void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Networks.DataManager.GetInstance().WriteLocalFile();
#else
            Application.Quit(); 
            Networks.DataManager.GetInstance().WriteLocalFile();
#endif
        }
    }
}


