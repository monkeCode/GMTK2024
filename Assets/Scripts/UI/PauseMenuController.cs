using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu;

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
    }
}