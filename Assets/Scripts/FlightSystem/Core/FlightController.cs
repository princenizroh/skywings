using UnityEngine;

namespace SkyWings.FlightSystem
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(FlightInputHandler))]
    [RequireComponent(typeof(TakeoffLandingController))]
    public class FlightController : MonoBehaviour
    {
        [SerializeField] private AircraftSO _data;
        [SerializeField] private Transform _visualBody;

        private Rigidbody _rb;
        private FlightInputHandler _input;
        private TakeoffLandingController _ground;

        private AircraftState _state = AircraftState.Grounded;
        private float _currentThrottle;
        private float _flightSpeed;
        private float _pitch;
        private float _roll;
        private float _yaw;
        private float _airborneTimer; 

        public AircraftState State    => _state;
        public float SpeedKmh         => _state == AircraftState.Flying ? _flightSpeed : _rb.velocity.magnitude * 3.6f;
        public float Altitude         => transform.position.y;
        public float Throttle         => _currentThrottle;

        private void Awake()
        {
            _rb     = GetComponent<Rigidbody>();
            _input  = GetComponent<FlightInputHandler>();
            _ground = GetComponent<TakeoffLandingController>();

            if (_data == null)
            {
                Debug.LogError("FlightController: AircraftSO belum di-assign di Inspector!");
                return;
            }

            _rb.mass         = _data.mass;
            _rb.drag         = _data.groundDrag;
            _rb.angularDrag  = _data.groundAngularDrag;
            _rb.useGravity   = true;
            _rb.centerOfMass = new Vector3(0f, _data.centerOfMassY, 0f);
            _rb.constraints  = RigidbodyConstraints.FreezeRotationX
                             | RigidbodyConstraints.FreezeRotationZ;
        }

        private void FixedUpdate()
        {
            switch (_state)
            {
                case AircraftState.Grounded:
                case AircraftState.Taxiing:
                    HandleGround();
                    break;
                case AircraftState.TakingOff:
                    HandleTakingOff();
                    break;
                case AircraftState.Flying:
                    HandleFlying();
                    break;
                case AircraftState.Landing:
                    HandleLanding();
                    break;
            }
        }

        private void HandleGround()
        {
            _rb.drag        = _data.groundDrag;
            _rb.useGravity  = true;
            _rb.constraints = RigidbodyConstraints.FreezeRotationX
                            | RigidbodyConstraints.FreezeRotationZ;

            // State: 
            _state = _rb.velocity.magnitude > 0.5f ? AircraftState.Taxiing : AircraftState.Grounded;

            // Reset visual body 
                _visualBody.localRotation = Quaternion.Lerp(
                    _visualBody.localRotation, Quaternion.Euler(0f, 180f, 0f),
                    Time.fixedDeltaTime * 8f);

            if (!_ground.IsOnGround) return;

            // Throttle
            if (_input.ThrottleUp)
                _currentThrottle = Mathf.MoveTowards(_currentThrottle, 1f, Time.fixedDeltaTime * _data.throttleRampUp);
            else
                _currentThrottle = Mathf.MoveTowards(_currentThrottle, 0f, Time.fixedDeltaTime * _data.throttleRampDown);

            _rb.AddForce(transform.forward * (_currentThrottle * _data.maxThrust), ForceMode.Force);

            var vel = _rb.velocity;
            vel.y = 0f;
            float fwd = Vector3.Dot(vel, transform.forward);
            _rb.velocity = transform.forward * Mathf.Max(fwd, 0f);

            var e = _rb.rotation.eulerAngles;
            _rb.rotation = Quaternion.Euler(0f, e.y, 0f);

            var av = _rb.angularVelocity;
            av.y = 0f;
            _rb.angularVelocity = av;

            // Steer
            float steer = _input.SteerInput;
            if (steer != 0f && SpeedKmh > 1f)
                _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0f, steer * _data.steerSpeed * Time.fixedDeltaTime, 0f));

            // brake
            if (_input.Brake)
            {
                var brake = -_rb.velocity.normalized * _data.brakingForce;
                brake.y = 0f;
                _rb.AddForce(brake, ForceMode.Force);
            }

            // TakingOff
            if (SpeedKmh >= _data.takeoffSpeed)
                _state = AircraftState.TakingOff;
        }

        private void HandleTakingOff()
        {
            HandleGround();

            // Gaya angkat makin besar seiring kecepatan naik
            float liftRatio = Mathf.Clamp01(SpeedKmh / _data.takeoffSpeed);
            _rb.AddForce(Vector3.up * (_data.liftForce * liftRatio), ForceMode.Force);

            if (!_ground.IsOnGround && SpeedKmh >= _data.takeoffSpeed)
            {
                _state       = AircraftState.Flying;
                _flightSpeed = SpeedKmh;
                _pitch       = 0f;
                _roll        = 0f;
                _yaw         = _rb.rotation.eulerAngles.y;
                _airborneTimer = 0f;
                _rb.useGravity   = false;
                _rb.drag         = 0f;
                _rb.angularDrag  = 0f;
                _rb.constraints  = RigidbodyConstraints.FreezeRotation;
            }
        }

        private void HandleFlying()
        {
            _rb.useGravity  = false;
            _rb.drag        = 0f;
            _rb.angularDrag = 0f;
            _rb.constraints = RigidbodyConstraints.FreezeRotation;

            // Speed control
            if (_input.ThrottleUp)
                _flightSpeed += _data.flightAccel * Time.fixedDeltaTime;
            else if (_input.ThrottleDown)
                _flightSpeed -= _data.flightBrake * Time.fixedDeltaTime;
            else
                _flightSpeed = Mathf.MoveTowards(_flightSpeed, _data.minFlightSpeed, _data.flightIdleDecel * Time.fixedDeltaTime);

            _flightSpeed = Mathf.Clamp(_flightSpeed, _data.minFlightSpeed, _data.maxFlightSpeed);

            // Arah terbang dihitung dari yaw + pitch agar pesawat benar-benar naik/turun
            Vector3 flightDir = Quaternion.Euler(_pitch, _yaw, 0f) * Vector3.forward;
            _rb.velocity = flightDir * (_flightSpeed / 3.6f);

            // Kontrol rotasi
            float pitchInput = _input.PitchInput;
            float rollInput  = _input.RollInput;
            float yawInput   = _input.YawInput;

            _pitch += pitchInput * _data.pitchSpeed * Time.fixedDeltaTime;
            _roll  += rollInput  * _data.rollSpeed  * Time.fixedDeltaTime;
            // Yaw: rudder langsung + auto-yaw dari bank
            _yaw   += yawInput   * _data.yawSpeed   * Time.fixedDeltaTime;
            _yaw   -= _roll      * _data.bankTurnFactor * Time.fixedDeltaTime;

            _pitch = Mathf.Clamp(_pitch, -_data.maxPitchAngle, _data.maxPitchAngle);
            _roll  = Mathf.Clamp(_roll,  -_data.maxRollAngle,  _data.maxRollAngle);

            if (rollInput == 0f)
                _roll = Mathf.MoveTowards(_roll, 0f, _data.autoLevelRoll * Time.fixedDeltaTime);
            if (pitchInput == 0f)
                _pitch = Mathf.MoveTowards(_pitch, 0f, _data.autoLevelPitch * Time.fixedDeltaTime);

            // Player root: hanya YAW — kamera tidak ikut miring
            _rb.MoveRotation(Quaternion.Euler(0f, _yaw, 0f));

            // Visual child (Airbus A310): pitch + roll sebagai localRotation
            if (_visualBody != null)
                _visualBody.localRotation = Quaternion.Euler(-_pitch, 180f, -_roll);

            // Transisi ke Landing — hanya setelah cukup lama di udara
            _airborneTimer += Time.fixedDeltaTime;
            if (_airborneTimer >= _data.minAirborneTime && _ground.IsOnGround)
                _state = AircraftState.Landing;
        }

        private void HandleLanding()
        {
            _rb.useGravity  = true;
            _rb.drag        = _data.groundDrag;
            _rb.constraints = RigidbodyConstraints.FreezeRotationX
                            | RigidbodyConstraints.FreezeRotationZ;

            // Reset sisa angular velocity dari Flying agar tidak muter sendiri
            _rb.angularVelocity = Vector3.zero;

            _rb.velocity = transform.forward * (_flightSpeed / 3.6f);
            _pitch = 0f;
            _roll  = 0f;
            var e = _rb.rotation.eulerAngles;
            _rb.rotation = Quaternion.Euler(0f, e.y, 0f);

            // Steer saat rollout — biar bisa koreksi arah di landasan
            float steer = _input.SteerInput;
            if (steer != 0f && _flightSpeed > 1f)
                _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0f, steer * _data.steerSpeed * Time.fixedDeltaTime, 0f));

            if (_visualBody != null)
                _visualBody.localRotation = Quaternion.Lerp(
                    _visualBody.localRotation, Quaternion.Euler(0f, 180f, 0f),
                    Time.fixedDeltaTime * 5f);

            _flightSpeed = Mathf.MoveTowards(_flightSpeed, 0f, _data.flightBrake * Time.fixedDeltaTime);

            if (_flightSpeed < _data.takeoffSpeed * 0.5f)
                _state = AircraftState.Taxiing;
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 380, 200));
            GUILayout.Label($"State   : {_state}");
            GUILayout.Label($"Speed   : {SpeedKmh:F0} km/h");
            GUILayout.Label($"Altitude: {Altitude:F0} m");
            GUILayout.Label($"Pitch   : {_pitch:F1}  Roll: {_roll:F1}  Yaw: {_yaw:F1}");
            GUILayout.Label($"Grounded: {_ground.IsOnGround}");
            GUILayout.EndArea();
        }
    }
}
