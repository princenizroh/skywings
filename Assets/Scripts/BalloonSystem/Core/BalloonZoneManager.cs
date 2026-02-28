using System.Collections.Generic;
using UnityEngine;

namespace SkyWings.BalloonSystem
{
    public class BalloonZoneManager : MonoBehaviour
    {
        public static BalloonZoneManager Instance { get; private set; }

        private List<BalloonZoneTrigger> _zones = new();

        public int TotalZones => _zones.Count;
        public int CompletedZones
        {
            get
            {
                int count = 0;
                foreach (var z in _zones)
                    if (z.IsCompleted) count++;
                return count;
            }
        }

        private void Awake()
        {
            Instance = this;
            _zones.AddRange(FindObjectsByType<BalloonZoneTrigger>(FindObjectsSortMode.None));
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(Screen.width - 200, 10, 190, 40));
            GUILayout.Label($"Balon: {CompletedZones} / {TotalZones}");
            GUILayout.EndArea();
        }
    }
}
