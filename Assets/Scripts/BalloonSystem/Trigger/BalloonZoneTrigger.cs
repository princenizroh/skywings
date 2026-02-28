using System;
using UnityEngine;

namespace SkyWings.BalloonSystem
{
    [RequireComponent(typeof(Collider))]
    public class BalloonZoneTrigger : MonoBehaviour
    {
        [SerializeField] private string zoneName = "Zone";

        public static BalloonZoneTrigger CurrentZone { get; private set; }

        public string ZoneName => zoneName;
        public bool IsCompleted { get; private set; }

        public event Action OnCompleted;

        public void MarkCompleted()
        {
            if (IsCompleted) return;
            IsCompleted = true;
            OnCompleted?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            CurrentZone = this;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            if (CurrentZone == this) CurrentZone = null;
        }
    }
}
