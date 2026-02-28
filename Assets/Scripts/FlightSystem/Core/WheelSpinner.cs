using UnityEngine;

namespace SkyWings.FlightSystem
{
    public class WheelSpinner : MonoBehaviour
    {
        [SerializeField] private Transform[] _mainWheels;

        [SerializeField] private Transform _noseWheel;

        [SerializeField] private float _wheelRadius = 0.5f;

        [SerializeField] private float _steerSpeed = 5f;

        [SerializeField] private float _maxSteerAngle = 45f;

        private FlightController _fc;
        private FlightInputHandler _input;
        private float _currentSteerAngle;

        private void Awake()
        {
            _fc = GetComponent<FlightController>();
            _input = GetComponent<FlightInputHandler>();
        }

        private void Update()
        {
            float speed = _fc != null ? _fc.SpeedKmh / 3.6f : 0f;

            float spinDeg = (speed / (2f * Mathf.PI * _wheelRadius)) * 360f * Time.deltaTime;

            foreach (var w in _mainWheels)
            {
                if (w == null) continue;
                w.Rotate(spinDeg, 0f, 0f, Space.Self);
            }

            if (_noseWheel != null)
            {
                _noseWheel.Rotate(spinDeg, 0f, 0f, Space.Self);

                float targetAngle = _input != null ? _input.SteerInput * _maxSteerAngle : 0f;
                _currentSteerAngle = Mathf.Lerp(
                    _currentSteerAngle, targetAngle, Time.deltaTime * _steerSpeed);

                var local = _noseWheel.localEulerAngles;
                _noseWheel.localEulerAngles = new Vector3(local.x, _currentSteerAngle, local.z);
            }
        }
    }
}
