using UnityEngine;

namespace SkyWings.MissionSystem
{
    public class MissionManager : MonoBehaviour
    {
        public static MissionManager Instance { get; private set; }

        [SerializeField] private float _hintDuration = 5f;

        private bool _finished;
        private float _hintTimer;

        private void Awake()
        {
            Instance = this;
            _hintTimer = _hintDuration;
        }

        private void Update()
        {
            if (_hintTimer > 0f)
                _hintTimer -= Time.deltaTime;
        }

        public void Finish(GameObject landingBox)
        {
            if (_finished) return;
            _finished = true;

            if (landingBox != null)
                landingBox.SetActive(false);
        }

        private void OnGUI()
        {
            if (_hintTimer > 0f)
            {
                float alpha = Mathf.Clamp01(_hintTimer);
                var hintStyle = new GUIStyle(GUI.skin.label)
                {
                    fontSize = 28,
                    fontStyle = FontStyle.Bold,
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = new Color(1f, 1f, 1f, alpha) }
                };
                GUI.Label(new Rect(0, Screen.height - 120, Screen.width, 100),
                    "Fly to the marked zones and drop a balloon!\nPress  B  to drop a balloon.", hintStyle);
            }

            if (!_finished) return;

            var style = new GUIStyle(GUI.skin.label)
            {
                fontSize = 48,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.MiddleCenter,
                normal = { textColor = Color.yellow }
            };
            GUI.Label(new Rect(0, Screen.height / 2 - 60, Screen.width, 120),
                "Congratulations!\nYou Finished the Game!", style);
        }
    }
}
