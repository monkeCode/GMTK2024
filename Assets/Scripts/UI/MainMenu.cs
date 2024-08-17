using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu: MonoBehaviour
    {
    
        public void StartGame()
        {
            SceneManager.LoadScene("BuildingDemo");
        }

        public void QuitGame()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}