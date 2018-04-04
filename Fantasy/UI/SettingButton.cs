/*
	Project : 	

	Author : NextLeaves

	Expression:

	Date:

	Repaird Record:

*/


using UnityEngine;
using UnityEngine.UI;

using Assets.Scripts.UI.Panel;

namespace Assets.Scripts.Fantasy.UI
{
    public class SettingButton : MonoBehaviour
    {
        public Button setting_btn;
        public GameObject settingPanel;
        public Button changing_btn;
        public GameObject changingPanel;
        public Button changingMode_btn;
        public GameObject changingModePanel;


        [Space]
        public Button info_btn;
        public Button music_btn;
        public Image musicblock_img;
        public Button cmusic_btn;
        public Button quit_btn;
        public GameObject QuitPanel;
        public Button okQuit_btn;
        public Button cancleQuit_btn;

        private AudioSource bg_audio;

        private void Awake()
        {
            bg_audio = GameObject.Find("SceneManagement/bgManagement").GetComponent<AudioSource>();
        }

        private void Start()
        {
            QuitPanel.SetActive(false);

            if (settingPanel.activeSelf) settingPanel.SetActive(!settingPanel.activeSelf);
            setting_btn.onClick.AddListener(OpenSettingPanel);
            changing_btn.onClick.AddListener(OpenChangingMusicPanel);
            changingMode_btn.onClick.AddListener(OpenChangingMusicModePanel);

            info_btn.onClick.AddListener(OpenInfoClick);
            music_btn.onClick.AddListener(OpenMusicClick);
            cmusic_btn.onClick.AddListener(OpenCMusicClick);
            quit_btn.onClick.AddListener(QuitingGame);

            
        }

        public void OpenSettingPanel()
        {
            settingPanel.SetActive(!settingPanel.activeSelf);
        }

        public void OpenChangingMusicPanel()
        {
            changingPanel.SetActive(!changingPanel.activeSelf);
            changingModePanel.SetActive(false);
        }

        public void OpenChangingMusicModePanel()
        {
            changingModePanel.SetActive(!changingModePanel.activeSelf);
        }

        void OpenInfoClick()
        {
            object[] obj = new object[2];
            obj[0] = "操作提示";
            obj[1] = "【T】动物旁，坐骑；【shift】加速；【V】切换视角";
            PanelManager._instance.OpenPanel<MentionPanel>("", obj);

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



        void QuitingGame()
        {
            QuitPanel.SetActive(true);
            okQuit_btn.onClick.AddListener(QuitedGame);
            cancleQuit_btn.onClick.AddListener(CancleQuitedGame);
        }

        void QuitedGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            Networks.DataManager.GetInstance().WriteLocalFile();
#else
            Application.Quit(); 
            Networks.DataManager.GetInstance().WriteLocalFile();
#endif
        }

        void CancleQuitedGame()
        {
            QuitPanel.SetActive(false);
        }
        
    }
}


