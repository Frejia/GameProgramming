using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to change the trails on the player when using Customization in Main Menu
/// </summary>
public class TrailManager : MonoBehaviour
{
    [Header("Refrences to the Trail Transforms on Player")]
    [SerializeField] private GameObject _leftTrailTransform;
    [SerializeField] private GameObject _rightTrailTransform;

    // Method for Button Press
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
