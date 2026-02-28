using UnityEngine;

namespace SkyWings.FlightSystem
{
    public class TakeoffLandingController : MonoBehaviour
    {
        [SerializeField] private Transform[] _wheels;

        [SerializeField] private LayerMask _groundLayer = ~0;

        [SerializeField] private float _rayLength = 0.5f;

        public bool IsOnGround { get; private set; }

        private void FixedUpdate()
        {
            IsOnGround = false;
            foreach (var wheel in _wheels)
            {
                if (wheel == null) continue;
                if (Physics.Raycast(wheel.position, Vector3.down, _rayLength, _groundLayer))
                {
                    IsOnGround = true;
                    break;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (_wheels == null) return;
            foreach (var wheel in _wheels)
            {
                if (wheel == null) continue;
                Gizmos.color = IsOnGround ? Color.green : Color.red;
                Gizmos.DrawLine(wheel.position, wheel.position + Vector3.down * _rayLength);
                Gizmos.DrawWireSphere(wheel.position, 0.15f);
            }
        }
    }
}

