using UnityEngine;

namespace SkyWings.FlightSystem
{
    [CreateAssetMenu(fileName = "AircraftData", menuName = "SkyWings/Flight/Aircraft Data")]
    public class AircraftSO : ScriptableObject
    {
        [Header("Ground")]
        public float mass = 3000f;
        public float maxThrust = 80000f;
        public float brakingForce = 30000f;
        public float steerSpeed = 40f;
        public float groundDrag = 0.3f;
        public float groundAngularDrag = 3f;
        public float takeoffSpeed = 60f; 

        [Header("Takeoff - Lift")]
        public float liftForce = 60000f; 
        public float minAirborneTime = 3f; 
        public float throttleRampUp = 0.8f; 
        public float throttleRampDown = 0.3f; 

        [Header("Physics")]
        public float centerOfMassY = -2f; 

        [Header("Flight - Speed")]
        public float flightAccel = 20f; 
        public float flightBrake = 40f; 
        public float flightIdleDecel = 5f;
        public float minFlightSpeed = 120f; 
        public float maxFlightSpeed = 500f; 

        [Header("Flight - Control")]
        public float pitchSpeed = 12f;
        public float rollSpeed = 40f;
        public float yawSpeed = 20f; 
        public float bankTurnFactor = 0.5f; 
        public float maxPitchAngle = 35f; 
        public float maxRollAngle = 75f; 

        [Header("Flight - Stability")]
        public float autoLevelRoll = 30f;
        public float autoLevelPitch = 8f; 
    }
}
