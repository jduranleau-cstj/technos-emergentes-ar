using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(MeshRenderer))]
public class Cursor : MonoBehaviour
{
    [SerializeField] private ARRaycastManager _raycastManager;
    [SerializeField] private float _rotation = 0f;
    private MeshRenderer _meshRenderer;

    void Start()
    {
        if (_raycastManager == null) {
            Debug.LogError("Le serializeField _raycastManager est requis.");
        }

        _meshRenderer = GetComponent<MeshRenderer>();

        // Cache le visuel le temps de trouver une surface
        _meshRenderer.enabled = false;
    }

    void Update()
    {
        // Liste vide de surfaces trouvées qui sera remplie par le raycast
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        // Position à partir de laquelle lancer le ray
        Vector2 position = new Vector2(Screen.width / 2f, Screen.height / 2f);
        // Vector2 position = Input.mousePosition;

        // Test de collision avec un ray
        _raycastManager.Raycast(position, hits, TrackableType.Planes);

        // Si au moins une surface est détectée
        if (hits.Count > 0) {
            // Affiche le visuel
            _meshRenderer.enabled = true;

            // Déplace l'objet à l'endroit trouvé par le Raycast
            transform.position = hits[0].pose.position;
            transform.rotation = hits[0].pose.rotation;

            // Tourne l'objet, au besoin, selon le serializeField
            transform.RotateAround(transform.position, transform.up, _rotation);
        
        // Si aucune surface est trouvée
        } else {
            // Cache le visuel
            _meshRenderer.enabled = false;
        }
    }
}
