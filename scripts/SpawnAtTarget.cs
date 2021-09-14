using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtTarget : MonoBehaviour
{

    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private GameObject _target;

    void Update()
    {
        // Si un premier doigt touche l'écran pour la première fois
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            // Crée une nouvelle copie du prefab à l'endroit où se trouve la cible
            Instantiate(_prefabToSpawn, _target.transform.position, _target.transform.rotation);
        }
    }
}
