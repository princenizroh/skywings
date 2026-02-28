using UnityEngine;
using SkyWings.BalloonSystem;

namespace SkyWings.MissionSystem
{
    [RequireComponent(typeof(Renderer))]
    public class MissionZoneVisual : MonoBehaviour
    {
        [SerializeField] private Material incompleteMaterial;
        [SerializeField] private Material completeMaterial;

        private Renderer _renderer;
        private bool _isComplete;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            if (incompleteMaterial != null)
                _renderer.material = incompleteMaterial;
        }

        private void Update()
        {
            if (_isComplete) return;
            var mgr = BalloonZoneManager.Instance;
            if (mgr == null) return;
            if (mgr.CompletedZones < mgr.TotalZones) return;

            _isComplete = true;
            if (completeMaterial != null)
                _renderer.material = completeMaterial;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isComplete) return;
            if (!other.CompareTag("Player")) return;

            MissionManager.Instance?.Finish(gameObject);
        }
    }
}
