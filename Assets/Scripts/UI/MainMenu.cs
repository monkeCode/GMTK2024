using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu: MonoBehaviour
    {
    
        public void StartGame()
        {
            SceneManager.LoadScene("Map test");
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}