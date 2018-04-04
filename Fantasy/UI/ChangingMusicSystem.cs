using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Fantasy.UI
{
    public class ChangingMusicSystem : MonoBehaviour
    {
        private enum OpenModes
        {
            Recycle,
            Simple,
            Random
        }

        public AudioSource musicSource;
        public List<AudioClip> music_list = new List<AudioClip>();
        private OpenModes mode = OpenModes.Random;

        public Button recycle_btn;
        public Button simple_btn;
        public Button random_btn;

        public Button openclose_btn;
        public Button last_btn;
        public Button next_btn;

        public SettingButton settingButton;

        private void Start()
        {
            if (musicSource == null)
                musicSource = GameObject.Find("SceneManagement/MusicManamgment").GetComponent<AudioSource>();
            recycle_btn.onClick.AddListener(OnRecycleClick);
            simple_btn.onClick.AddListener(OnSimpleClick);
            random_btn.onClick.AddListener(OnRandomClick);

            openclose_btn.onClick.AddListener(OpenCLoseMusic);
            last_btn.onClick.AddListener(OnOpenLastMusicClick);
            next_btn.onClick.AddListener(OnOpenNextMusicClick);
        }

        private void OnRecycleClick()
        {
            SetOpenMusicMode(OpenModes.Recycle);
            settingButton.OpenChangingMusicModePanel();
        }

        private void OnSimpleClick()
        {
            SetOpenMusicMode(OpenModes.Simple);
            settingButton.OpenChangingMusicModePanel();
        }

        private void OnRandomClick()
        {
            SetOpenMusicMode(OpenModes.Random);
            settingButton.OpenChangingMusicModePanel();
        }

        private void OnOpenNextMusicClick()
        {
            OpenNextLastMusic(true);
        }

        private void OnOpenLastMusicClick()
        {
            OpenNextLastMusic(false);
        }

        private void OpenCLoseMusic()
        {
            if (musicSource.isPlaying)
                musicSource.Stop();
            else
                musicSource.Play();
        }

        private void SetOpenMusicMode(OpenModes openMode)
        {
            mode = openMode;
        }

        private void OpenNextLastMusic(bool isNext = true)
        {
            if (!musicSource.isPlaying) OpenCLoseMusic();
            switch (mode)
            {
                case OpenModes.Random:
                    int index = UnityEngine.Random.Range(0, music_list.Count - 1);
                    musicSource.clip = music_list[index];
                    musicSource.loop = false;
                    break;
                case OpenModes.Recycle:
                    musicSource.loop = true;
                    break;
                case OpenModes.Simple:
                    if (isNext)
                    {
                        int lastindex = music_list.IndexOf(musicSource.clip) + 1;
                        if (lastindex == music_list.Count - 1) lastindex = 0;
                        musicSource.clip = music_list[lastindex];
                    }
                    else
                    {
                        int lastindex = music_list.IndexOf(musicSource.clip) - 1;
                        if (lastindex <= 0) lastindex = music_list.Count - 1;
                        musicSource.clip = music_list[lastindex];
                    }
                    musicSource.loop = false;
                    break;
            }
            musicSource.Play();
        }


    }

}
