using UnityEngine;
using UnityEngine.SceneManagement;


namespace SkyWings.PauseSystem
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private string gameplayScene = "MainMenu";

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(gameplayScene);        
        }
    }
}
