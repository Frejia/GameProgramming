using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


/// <summary>
/// Changes Controls for the players
///
/// Used in Main Menu
/// </summary>
public class SetControls : MonoBehaviour
{
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private Material mat1;
    [SerializeField] private Material mat2;
    [SerializeField] private Material outlineMat;
    //[SerializeField] private GameObject player2Prefab;

    [SerializeField] private GameObject playerModelHolderTransform;

    [SerializeField] private Slider player1Sensitivity;
    //[SerializeField] private Slider player2Sensitivity;

    // Set Control Scheme on Player Prefab
    public void SetScheme(bool chosen)
    {
        if (chosen)
        {
            Debug.Log("Switching to Keyboard control scheme");
            // Check if Keyboard.current is null
            Debug.Log(Keyboard.current);

            player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            player2Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Controller", Gamepad.current);
        }
        else
        {
            Debug.Log("Switching to Controller control scheme");
            Debug.Log(Gamepad.current);
            player1.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Controller", Gamepad.current);
            player2Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
        }
    }

    // Change Sensitivity Settings for pitch and yaw
    public void SetSensitivity()
    {
        player1.GetComponent<ShipMovement>().yawTorque = player1Sensitivity.value;
        player1.GetComponent<ShipMovement>().pitchTorque = player1Sensitivity.value;
       // player2Prefab.GetComponent<ShipMovement>().yawTorque = sensitivity;
       // player2Prefab.GetComponent<ShipMovement>().pitchTorque = sensitivity;
    }

    public void SetModeColor1(Material newMat)
    {
        Renderer[] renderers = playerModelHolderTransform.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;

            foreach (Material material in materials)
            {
                //delete every material
                Destroy(material);
            }

            //add new material
            materials[0] = outlineMat;
            materials[1] = newMat;
            
            
            renderer.materials = materials;
        }
    }
    

    public void SetModeColor2(Material newMat)
    {
        //mat2 = newMat;
        SetModeColor1(newMat);
    }

    public void SetPlayerModel(GameObject model)
    {
        player1.transform.GetChild(0).GetComponent<MeshFilter>().mesh = model.GetComponent<MeshFilter>().mesh;
        player1.transform.GetChild(0).GetComponent<MeshRenderer>().materials = model.GetComponent<MeshRenderer>().materials;
    }
    
    
}