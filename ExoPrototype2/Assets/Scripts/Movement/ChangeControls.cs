using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject player2Prefab;

    [SerializeField] private Slider player1Sensitivity;
    [SerializeField] private Slider player2Sensitivity;

    // Set Control Scheme on Player Prefab
    public void SetScheme(bool chosen)
    {
        if (chosen)
        {
            //Keyboard
            Debug.Log("Made a Keyboard player");
            // var p1 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Keyboard");
            player1Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
            player2Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Controller", Gamepad.current);
          
            
        }
        else
        {
            //Controller
            Debug.Log("Made a Controller player");
            // var p2 = PlayerInput.Instantiate(playerPrefab, controlScheme: "Controller");
            player1Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Controller", Gamepad.current);
            player2Prefab.GetComponent<PlayerInput>().SwitchCurrentControlScheme("Keyboard", Keyboard.current);
        }
    }

    // Change Sensitivity Settings for pitch and yaw
    public void SetSensitivity(float sensitivity)
    {
        player1Prefab.GetComponent<ShipMovement>().yawTorque = sensitivity;
        player1Prefab.GetComponent<ShipMovement>().pitchTorque = sensitivity;
        player2Prefab.GetComponent<ShipMovement>().yawTorque = sensitivity;
        player2Prefab.GetComponent<ShipMovement>().pitchTorque = sensitivity;
    }
    
}