using UnityEngine;

namespace SkyWings.FlightSystem
{
    public class FlightInputHandler : MonoBehaviour
    {
        [Header("Key Bindings")]
        [SerializeField] private KeyCode _throttleUp = KeyCode.W;
        [SerializeField] private KeyCode _throttleDown = KeyCode.S;
        [SerializeField] private KeyCode _brake = KeyCode.Space;
        [SerializeField] private KeyCode _pitchUp = KeyCode.Q;
        [SerializeField] private KeyCode _pitchDown = KeyCode.E;
        [SerializeField] private KeyCode _rollLeft = KeyCode.A;
        [SerializeField] private KeyCode _rollRight = KeyCode.D;
        [SerializeField] private KeyCode _yawLeft = KeyCode.Z;
        [SerializeField] private KeyCode _yawRight = KeyCode.X;
        [SerializeField] private KeyCode _dropBalloon = KeyCode.B;
        [SerializeField] private KeyCode _takePhoto = KeyCode.F;
        [SerializeField] private KeyCode _switchCamera = KeyCode.C;
        [SerializeField] private KeyCode _galleryToggle = KeyCode.Tab;

        public bool ThrottleUp => Input.GetKey(_throttleUp);
        public bool ThrottleDown => Input.GetKey(_throttleDown);
        public bool Brake => Input.GetKey(_brake) || Input.GetKey(_throttleDown);
        public float SteerInput => Input.GetKey(_rollLeft) ? -1f : Input.GetKey(_rollRight) ? 1f : 0f;
        public float PitchInput => Input.GetKey(_pitchUp) ? 1f : Input.GetKey(_pitchDown) ? -1f : 0f;
        public float RollInput => Input.GetKey(_rollLeft) ? 1f : Input.GetKey(_rollRight) ? -1f : 0f;
        public float YawInput => Input.GetKey(_yawLeft) ? -1f : Input.GetKey(_yawRight) ? 1f : 0f;

        public bool DropBalloonPressed { get; private set; }
        public bool TakePhotoPressed { get; private set; }
        public bool SwitchCameraPressed { get; private set; }
        public bool GalleryTogglePressed { get; private set; }

        private void Update()
        {
            DropBalloonPressed = Input.GetKeyDown(_dropBalloon);
            TakePhotoPressed = Input.GetKeyDown(_takePhoto);
            SwitchCameraPressed = Input.GetKeyDown(_switchCamera);
            GalleryTogglePressed = Input.GetKeyDown(_galleryToggle);}
    }
}
