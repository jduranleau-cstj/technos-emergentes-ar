using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
public class MultipleTracking : MonoBehaviour
{
    [SerializeField] private GameObject[] _prefabs;
    [SerializeField] private bool updateRotation = false;

    private Dictionary<string, GameObject> _spawnedPrefabs = new Dictionary<string, GameObject>();
    private ARTrackedImageManager _imageManager;

    void Awake()
    {
        _imageManager = GetComponent<ARTrackedImageManager>();

        // Crée une instance de chaque prefab pour une utilisation future
        foreach (GameObject prefab in _prefabs) {
            GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            instance.name = prefab.name;
            instance.SetActive(false);
            _spawnedPrefabs.Add(prefab.name, instance);
        }
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
        // Si le tracker détecté n'a pas de prefab avec le même nom, on ne fait rien
        if (_spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name) == false) {
            return;
        }

        // référence vers l'instance du prefab associé au tracker
        GameObject prefab = _spawnedPrefabs[trackedImage.referenceImage.name];

        if (trackedImage.trackingState == TrackingState.Limited || trackedImage.trackingState == TrackingState.None) {
            // Debug.Log("Hide " + trackedImage.referenceImage.name + " with state " + trackedImage.trackingState);
            prefab.SetActive(false);
        } else {
            // Debug.Log("Show " + trackedImage.referenceImage.name + " with state " + trackedImage.trackingState);
            prefab.SetActive(true);
            prefab.transform.position = trackedImage.transform.position;
            if (updateRotation) {
                prefab.transform.rotation = trackedImage.transform.rotation;
            }
        }
    }
}
