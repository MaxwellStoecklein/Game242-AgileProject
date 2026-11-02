using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;
    
    public InputSystem_Actions PlayerControls;

    private bool awaitState = false;

    [SerializeField] private TurnSystem turnSystem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerControls = new InputSystem_Actions();
        PlayerControls.Enable();

        #region Link_Control_Functions

        PlayerControls.UI.Move.performed += MenuNav;
        PlayerControls.UI.Attack.performed += Select;
        PlayerControls.UI.Back.performed += Back;
        PlayerControls.UI.Menu.performed += OpenMenu;

        PlayerControls.Player.Menu.performed += OpenMenu;
        PlayerControls.Player.Move.performed += MovePlayer;
        PlayerControls.Player.Attack.performed += StandardAttack;

        /*PlayerControls.ErrorChecker.Move.performed += ctx => NoControlEnabled();
        PlayerControls.ErrorChecker.Attack.performed += ctx => NoControlEnabled();
        PlayerControls.ErrorChecker.Menu.performed += ctx => NoControlEnabled();
        PlayerControls.ErrorChecker.Back.performed += ctx => NoControlEnabled();*/

        #endregion
        
        // Player should start with world controls mode by default.
        PlayerControls.UI.Disable();
        
        // ErrorChecker should ALWAYS be enabled. This alerts when no valid control mode is enabled, which should
        // NEVER happen. 
        PlayerControls.ErrorChecker.Enable();

    }

    public void SetToMoveMode()
    {
        PlayerControls.UI.Disable();
        PlayerControls.Player.Enable();
    }

    public void SetToMenuMode()
    {
        PlayerControls.UI.Enable();
        PlayerControls.Player.Disable();
    }

    public void SetToAwaitMode()
    {
        PlayerControls.UI.Disable();
        PlayerControls.Player.Disable();
    }

    public void InitTurnSystem(TurnSystem ts)
    {
        turnSystem = ts;
    }
    
    void MovePlayer(InputAction.CallbackContext input)
    {
        turnSystem.RequestPlayerMove(input.ReadValue<Vector2>());
        Debug.Log($"Requested move with input: {input.ReadValue<Vector2>()}");
        SetToAwaitMode();
    }

    void StandardAttack(InputAction.CallbackContext input)
    {
        turnSystem.RequestPlayerStandardAttack();
        SetToAwaitMode();
    }

    void MenuNav(InputAction.CallbackContext input)
    {
        UIManager.instance.MoveCursor(input.ReadValue<Vector2>());
    }

    void Select(InputAction.CallbackContext input)
    {
        UIManager.instance.SelectElement();
    }

    void Back(InputAction.CallbackContext input)
    {
        UIManager.instance.Back();
    }

    void OpenMenu(InputAction.CallbackContext input)
    {
        if (PlayerControls.UI.enabled)
        {
            UIManager.instance.FullExit();
            SetToMoveMode();
        }
        else if (PlayerControls.Player.enabled)
        {
            UIManager.instance.OpenMenu();
            SetToMenuMode();
        }
    }

    void NoControlEnabled()
    {
        if (!PlayerControls.Player.enabled && !PlayerControls.UI.enabled)
        {
            Debug.LogWarning("NO CONTROL SCHEME ENABLED!!!");
        }
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
