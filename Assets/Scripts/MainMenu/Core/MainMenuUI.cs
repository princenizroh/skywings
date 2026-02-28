using UnityEngine;
using UnityEngine.SceneManagement;

namespace SkyWings.MainMenu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private string gameplayScene = "Gameplay";

        private void Start()
        {
            Time.timeScale = 1f;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(gameplayScene);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
