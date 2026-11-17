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
        
        /*
         * TODO: Potentially refactor these functions to be part of the update method
         * in order to ensure the game never misses an input. I fear that poorly timed frame stutters
         * may cause it to miss a started/canceled signal.
         */
        PlayerControls.Player.MoveModifier.started += MoveModifierOn;
        PlayerControls.Player.MoveModifier.canceled += MoveModifierOff;
        PlayerControls.Player.AimModifier.started += AimModifierOn;
        PlayerControls.Player.AimModifier.canceled += AimModifierOff;
        

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
        if (PlayerControls.Player.MoveModifier.inProgress)
        {
            if (/*PlayerControls.Player.Move.*/true)
            {
                
            }
        }
        
        turnSystem.RequestPlayerMove(input.ReadValue<Vector2>());
        Debug.Log($"Requested move with input: {input.ReadValue<Vector2>()}");
    }

    void CheckDiagonalInput(InputAction.CallbackContext input)
    {
        
    }

    void StandardAttack(InputAction.CallbackContext input)
    {
        turnSystem.RequestPlayerStandardAttack();
        SetToAwaitMode();
    }

    void MoveModifierOn(InputAction.CallbackContext input)
    {
        
    }

    void MoveModifierOff(InputAction.CallbackContext input)
    {
        
    }

    void AimModifierOn(InputAction.CallbackContext input)
    {
        
    }

    void AimModifierOff(InputAction.CallbackContext input)
    {
        
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
    
    
}
