using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject _objectToSpawn;
    [SerializeField] private GameObject _target;

    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) {
            Instantiate(_objectToSpawn, _target.transform.position, _target.transform.rotation);
            Debug.Log("Lapin");
        }
    }
}
