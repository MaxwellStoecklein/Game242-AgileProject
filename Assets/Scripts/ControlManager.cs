using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public InputSystem_Actions PlayerControls;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControls = new InputSystem_Actions();
        PlayerControls.Enable();
        PlayerControls.Player.Move.performed += DirectionInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /*
     * left: x > -30 && x <= 30
       up left: x > 30 && x <= 60
       up: x > 60 && x <= 120
       up right: x > 120 && x <= 150
       right: x > 150 && x <= -150
       down right: x > -150 && x <= -120
       down: x > -120 && x <= -60
       down left: x > -60 && x <= -30
     */
    void DirectionInput(InputAction.CallbackContext input)
    {
        Vector2 dir = input.ReadValue<Vector2>();
        dir.Normalize();
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (angle > -30f && angle <= 30f)
        {
            Debug.Log("Left");
        }
        if (angle > 30f && angle <= 60f)
        {
            Debug.Log("Up Left");
        }
        if (angle > 60f && angle <= 120f)
        {
            Debug.Log("Up");
        }
        if (angle > 120f && angle <= 150f)
        {
            Debug.Log("Up Right");
        }
        if (angle > 150f || angle <= -150f)
        {
            Debug.Log("Right");
        }
        if (angle > -150f && angle <= -120f)
        {
            Debug.Log("Down Right");
        }
        if (angle > -120f && angle <= -60f)
        {
            Debug.Log("Down");
        }
        if (angle > -60f && angle <= -30f)
        {
            Debug.Log("Down Left");
        }
    }
}
