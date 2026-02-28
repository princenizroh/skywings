using Cinemachine;
using UnityEngine;
using SkyWings.FlightSystem;

namespace SkyWings.CameraSystem
{
    public class CameraModeSwitcher : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _thirdPersonCam;
        [SerializeField] private CinemachineVirtualCamera _noseCam;

        private FlightInputHandler _input;
        private bool _isNoseCam;

        private void Awake()
        {
            _input = GetComponent<FlightInputHandler>();
        }

        private void Start()
        {
            SetNoseCam(false);
        }

        private void Update()
        {
            if (_input != null && _input.SwitchCameraPressed)
                SetNoseCam(!_isNoseCam);
        }

        private void SetNoseCam(bool noseActive)
        {
            _isNoseCam = noseActive;
            _thirdPersonCam.Priority = noseActive ? 5 : 10;
            _noseCam.Priority = noseActive ? 10 : 5;
        }
    }
}
