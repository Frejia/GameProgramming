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
    [SerializeField] private GameObject player1Prefab;
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
            //Keyboard
            Debug.Log("Made a Keyboard player");
            // var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard");
            player1Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            //player2Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Controller", Gamepad.current);
          
            
        }
        else
        {
            //Controller
            Debug.Log("Made a Controller player");
            // var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Controller");
            player1Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Controller", Gamepad.current);
           // player2Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
        }
    }

    // Change Sensitivity Settings for pitch and yaw
    public void SetSensitivity()
    {
        player1Prefab.GetComponent<ShipMovement>().yawTorque = player1Sensitivity.value;
        player1Prefab.GetComponent<ShipMovement>().pitchTorque = player1Sensitivity.value;
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
        player1Prefab.transform.GetChild(0).GetComponent<MeshFilter>().mesh = model.GetComponent<MeshFilter>().mesh;
        player1Prefab.transform.GetChild(0).GetComponent<MeshRenderer>().materials = model.GetComponent<MeshRenderer>().materials;
    }
    
    
}