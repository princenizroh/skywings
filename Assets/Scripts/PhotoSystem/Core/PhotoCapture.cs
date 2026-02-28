using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SkyWings.FlightSystem;

namespace SkyWings.PhotoSystem
{
    public class PhotoCapture : MonoBehaviour
    {
        public static PhotoCapture Instance { get; private set; }

        private FlightInputHandler _input;
        private List<Texture2D> _photos = new();

        public IReadOnlyList<Texture2D> Photos => _photos;

        public event Action OnPhotoCaptured;

        private void Awake()
        {
            Instance = this;
            _input = GetComponent<FlightInputHandler>();
        }

        private void Update()
        {
            if (_input == null || !_input.TakePhotoPressed) return;
            StartCoroutine(CapturePhoto());
        }

        private IEnumerator CapturePhoto()
        {
            yield return new WaitForEndOfFrame();

            Texture2D photo = ScreenCapture.CaptureScreenshotAsTexture();
            _photos.Add(photo);
            OnPhotoCaptured?.Invoke();
            Debug.Log($"Photo captured! Total: {_photos.Count}");
        }
    }
}
