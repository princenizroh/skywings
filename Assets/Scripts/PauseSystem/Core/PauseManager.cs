using UnityEngine;

namespace SkyWings.PauseSystem
{
    public class PauseManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();
        }
    }
}
