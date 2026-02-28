using UnityEngine;

namespace SkyWings.MissionSystem
{
    [RequireComponent(typeof(Collider))]
    public class MissionStartTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            gameObject.SetActive(false);
        }
    }
}
