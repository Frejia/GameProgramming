using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraInitalizer : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private GameObject virtualPlayerCam;
    
    // Start is called before the first frame update
    void Start()
    {
        ShipMovement[] characterMovements = FindObjectsOfType<ShipMovement>();
        int layer;
        if (characterMovements.Length == 1)
        {
            // When there is 1 player, then the layer is the first player
            layer = 8;
        }
        else
        {
            //When there is a 2nd player, then the layer is the second player
            layer = 13;
        }
        
        virtualPlayerCam.layer = layer;
        var bitMask = (1 << layer)
                      | (1 << 0)
                      | (1 << 1)
                      | (1 << 2)
                      | (1 << 4)
                      | (1 << 5)
                      | (1 << 8);
        
        cam.cullingMask = bitMask;
        cam.gameObject.layer = layer;
    }
    
}
