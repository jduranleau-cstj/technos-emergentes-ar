using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageTrigger : MonoBehaviour
{

    private ARTrackedImageManager _imageManager;

    [SerializeField] UnityEvent<string> _trackerFound = new UnityEvent<string>();
    [SerializeField] UnityEvent<string> _trackerLost = new UnityEvent<string>();

    void Awake()
    {
        _imageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        _imageManager.trackedImagesChanged += ImageChanged;
    }

    private void OnDisable()
    {
        _imageManager.trackedImagesChanged -= ImageChanged;
    }

    private void ImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateImage(trackedImage);
        }
        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            UpdateImage(trackedImage);
        }
    }

    private void UpdateImage(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None) {
            _trackerLost.Invoke(trackedImage.referenceImage.name);
        } else {
            _trackerFound.Invoke(trackedImage.referenceImage.name);
        }

    }
}
