using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// To rotate Loading Bar
/// </summary>
public class UIRotate : MonoBehaviour
{
    // Rotate Object on Z Axis until stopped
    public float speed = -20f;
    public bool rotate = true;
    
    // Update is called once per frame
    void Update()
    {
        if (rotate)
        {
            transform.Rotate(Vector3.forward * (speed * Time.deltaTime));
        }
    }
    
}
