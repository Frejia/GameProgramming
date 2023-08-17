using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    [SerializeField] private GameObject _leftTrailTransform;
    [SerializeField] private GameObject _rightTrailTransform;

    public void SetTrail(GameObject trail)
    {
        // delete old trail
        foreach (Transform child in _leftTrailTransform.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in _rightTrailTransform.transform)
        {
            Destroy(child.gameObject);
        }
        
        // create new trail
        Instantiate(trail, _leftTrailTransform.transform.position, Quaternion.identity, _leftTrailTransform.transform);
        Instantiate(trail, _rightTrailTransform.transform.position, Quaternion.identity, _rightTrailTransform.transform);
    }
}