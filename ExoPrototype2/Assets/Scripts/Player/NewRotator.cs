using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Formerly Rotator
///
/// Handles rotation of player around own axis when moving left or right
/// </summary>
public class NewRotator : MonoBehaviour
{
    /// <summary>
    /// Possible rotations for player object
    /// </summary>
    public enum RotationDirection
    {
        Left,
        Right,
        Neutral,
        Other,
        None
    }
    
    private RotationDirection currentDirection;
    private float strafe1D;
    
    private void FixedUpdate()
    {
        HandleRotation();
        //Debug.Log(Time.fixedDeltaTime);
    }

    /// <summary>
    /// Rotate player according to left and right movement
    /// </summary>
    private void HandleRotation()
    {
        switch (strafe1D)
        {
            case > 0.1f:
                transform.Rotate(Vector3.right * 100f * Time.fixedDeltaTime);
                break;
            case < -0.1f:
                transform.Rotate(Vector3.left * 100f * Time.fixedDeltaTime);
                break;
            default:
                //rotate back to neutral rot
                break;
        }
    }
    
    public void SetStrafe(float strafe)
    {
        strafe1D = strafe;
    }
}
