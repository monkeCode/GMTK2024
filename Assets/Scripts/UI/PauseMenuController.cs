using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

        [SerializeField] private Slider _sfxSlider;
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private AudioMixer _musicMixer;

        private void Update()
        {
            HandleInput();
        }

        public void ToMainMenu()
        {
            SceneManager.LoadScene(Constants.SceneNames.MainMenu);
        }

        public void Open()
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }

        public void Close()
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }

        public void Restart()
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex ) ;
        }

        private void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseMenu.activeSelf)
                {
                    Close();
                }
                else
                {
                    Open();
                }
            }
        }

        public void onSfxChanged()
        {
            _musicMixer.SetFloat("sfxVol", _sfxSlider.value*80-80);
        }

        public void onMusicChanged()
        {
            _musicMixer.SetFloat("musicVol", _musicSlider.value*80-80);
        }

    }
}