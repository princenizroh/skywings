using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SkyWings.FlightSystem;

namespace SkyWings.PhotoSystem
{
    public class PhotoGalleryUI : MonoBehaviour
    {
        [Header("Gallery")]
        [SerializeField] private GameObject _galleryPanel;
        [SerializeField] private Transform _gridContainer;
        [SerializeField] private RawImage _photoCellPrefab;

        [Header("Capture Notif")]
        [SerializeField] private TextMeshProUGUI _notifText;
        [SerializeField] private float _notifDuration = 1.5f;

        private FlightInputHandler _input;
        private bool _isOpen;

        private void Awake()
        {
            if (_input == null)
                _input = FindAnyObjectByType<FlightInputHandler>();
        }
        private void Start()
        {
            _galleryPanel.SetActive(false);
            _notifText.gameObject.SetActive(false);
            PhotoCapture.Instance.OnPhotoCaptured += ShowNotif;
        }

        private void Update()
        {
            if (_input != null && _input.GalleryTogglePressed)
                ToggleGallery();
        }

        private void ShowNotif()
        {
            StopAllCoroutines();
            StartCoroutine(NotifRoutine());
        }

        private IEnumerator NotifRoutine()
        {
            _notifText.text = "Capture Successfully!";
            _notifText.gameObject.SetActive(true);
            yield return new WaitForSeconds(_notifDuration);
            _notifText.gameObject.SetActive(false);
        }

        private void ToggleGallery()
        {
            _isOpen = !_isOpen;
            _galleryPanel.SetActive(_isOpen);

            if (_isOpen)
                RefreshGallery();

            Time.timeScale = _isOpen ? 0f : 1f;
        }

        private void RefreshGallery()
        {
            foreach (Transform child in _gridContainer)
                Destroy(child.gameObject);

            foreach (var photo in PhotoCapture.Instance.Photos)
            {
                var cell = Instantiate(_photoCellPrefab, _gridContainer);
                cell.texture = photo;
            }
        }

        private void OnDestroy()
        {
            if (PhotoCapture.Instance != null)
                PhotoCapture.Instance.OnPhotoCaptured -= ShowNotif;
            Time.timeScale = 1f;
        }
    }
}


