using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    [SerializeField] public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    [SerializeField] private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;

    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.fixedDeltaTime);
            
        transform.Rotate(-mouseDistance.y * lookRotateSpeed * Time.fixedDeltaTime, mouseDistance.x * lookRotateSpeed * Time.fixedDeltaTime, rollInput * rollSpeed * Time.fixedDeltaTime, Space.Self);
        
        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.fixedDeltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.fixedDeltaTime);
        activeHoverSpeed =  Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.fixedDeltaTime);

       // while (activeForwardSpeed == 0) { this.GetComponent<Rigidbody>().useGravity = true; }
        
        transform.position += transform.forward * activeForwardSpeed * Time.fixedDeltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.fixedDeltaTime) + (transform.up * activeHoverSpeed * Time.fixedDeltaTime);

    }
}
