using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [Header("Rotator Settings")]
    [SerializeField]
    private bool isRotating = false;

    private Quaternion startRotation { get; set; }
    private Quaternion endRotation { get; set; }
    [SerializeField]
    private float rotationSpeed = 90.0f;
    
    public void StartRotating(Quaternion startRot, Quaternion endRot)
    {
        if (!isRotating)
        {
            startRotation = startRot;
            endRotation = endRot;
            isRotating = true;
        }
    }
    public void StopRotating()
    {
        endRotation = startRotation;
    }
    
    private void FixedUpdate()
    {
        if (isRotating)
        {
            float step = rotationSpeed * Time.fixedDeltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, endRotation, step);
            // Check if the rotation has reached the endRotation
            if (transform.rotation == endRotation)
            {
                isRotating = false;
            } 
        }
    }
    
    
    
}
