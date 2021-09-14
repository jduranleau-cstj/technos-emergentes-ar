using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnAtTarget : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject _prefabToSpawn;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            Vector2 position = Input.mousePosition;
            _raycastManager.Raycast(position, hits, TrackableType.Planes);

            if (hits.Count > 0) {
                Instantiate(_prefabToSpawn, hits[0].pose.position, hits[0].pose.rotation);
            } else {
                // Pas de surface!
            }
        }
    }
}
