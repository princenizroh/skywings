using UnityEngine;
using SkyWings.FlightSystem;

namespace SkyWings.BalloonSystem
{
    public class BalloonDropper : MonoBehaviour
    {
        [SerializeField] private GameObject balloonPrefab;

        private FlightInputHandler _input;

        private void Awake()
        {
            _input = GetComponent<FlightInputHandler>();
        }

        private void Update()
        {
            if (_input == null || !_input.DropBalloonPressed) return;

            var zone = BalloonZoneTrigger.CurrentZone;
            if (zone == null || zone.IsCompleted) return;

            zone.MarkCompleted();

            if (balloonPrefab != null)
            {
                Instantiate(balloonPrefab, transform.position, Quaternion.identity);
            }

            Debug.Log($"Balloon dropped at '{zone.ZoneName}'");
        }
    }
}
