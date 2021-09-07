using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Cursor : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private GameObject _visual;

    void Start()
    {
        _visual.SetActive(false);
    }

    void Update()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 position = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Vector2 position = Input.mousePosition;
        _raycastManager.Raycast(position, hits, TrackableType.Planes);

        if (hits.Count > 0) {
            _visual.transform.position = hits[0].pose.position;
            _visual.transform.rotation = hits[0].pose.rotation;
            _visual.transform.RotateAround(_visual.transform.position, _visual.transform.up, 180f);
            _visual.SetActive(true);
        } else {
            _visual.SetActive(false);
        }
    }
}
