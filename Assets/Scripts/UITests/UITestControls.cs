using System.Collections.Generic;
using UnityEngine;

/*
 * This script is only meant to control the UI in a test scene. Real controls should be run through the ControlManager
 * script.
 */

public class UITestControls : MonoBehaviour
{
    public InputSystem_Actions testActs = new InputSystem_Actions();
    public UIManager uiManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        testActs.Enable();
        testActs.Player.Disable();
        testActs.UI.Enable();
        
        testActs.UI.Navigate.performed += ctx => uiManager.MoveCursor(ctx.ReadValue<Vector2>());
        testActs.UI.Submit.performed += ctx => uiManager.SelectElement();
        testActs.UI.Cancel.performed += ctx => uiManager.Back();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
