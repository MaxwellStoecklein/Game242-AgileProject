using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlManager : MonoBehaviour
{
    public static ControlManager instance;
    
    public InputSystem_Actions PlayerControls;

    private bool moveMode = false;
    private bool uiMode = false;

    private float timeFromLastRot = 0f;
    private Vector2 lastMoveDir = Vector2.zero;
    private Vector2 lastDiagonalMoveDir = Vector2.zero;

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

        // NOTE: The order these are listed in is important. Attaching Select to Attack.performed
        // before attaching StandardAttack to Attack.performed will cause the player to instantly use a
        // standard attack when trying to exit the main menu by selecting the "Back" option as opposed to
        // pressing the Back or OpenMenu inputs to close it.
        
        PlayerControls.Player.Move.performed += MovePlayer;
        PlayerControls.Player.Move.canceled += DiagonalAimFilter;
        PlayerControls.Player.Attack.performed += StandardAttack;
        
        PlayerControls.Player.Move.performed += MenuNav;
        PlayerControls.Player.Attack.performed += Select;
        PlayerControls.Player.Back.performed += Back;
        PlayerControls.Player.Menu.performed += OpenMenu;
        
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
        moveMode = true;
        uiMode = false;
        
        // TODO: Remove UI control mode. Caused issues reading inputs.
        PlayerControls.UI.Disable();
        
        // ErrorChecker should ALWAYS be enabled. This alerts when no valid control mode is enabled, which should
        // NEVER happen. 
        PlayerControls.ErrorChecker.Enable();

    }

    public void SetToMoveMode()
    {
        moveMode = true;
        uiMode = false;
        Debug.Log("SetMode: Move");
        /*PlayerControls.UI.Disable();
        PlayerControls.Player.Enable();*/
    }

    public void SetToMenuMode()
    {
        uiMode = true;
        moveMode = false;
        Debug.Log("SetMode: UI");
        /*PlayerControls.UI.Enable();
        PlayerControls.Player.Disable();*/
    }

    public void SetToAwaitMode()
    {
        moveMode = false;
        uiMode = false;
        Debug.Log("SetMode: Await");
        /*PlayerControls.UI.Disable();
        PlayerControls.Player.Disable();*/
    }

    public void InitTurnSystem(TurnSystem ts)
    {
        turnSystem = ts;
    }
    
    void MovePlayer(InputAction.CallbackContext input)
    {
        if (moveMode)
        {
            Vector2 dir = input.ReadValue<Vector2>();
            if (PlayerControls.Player.MoveModifier.inProgress)
            {
                if (CheckDiagonalInput(dir))
                {
                    turnSystem.RequestPlayerMove(dir);
                    Debug.Log($"Requested DIAGONAL move with input: {input.ReadValue<Vector2>()}");
                }
            }
            else if (PlayerControls.Player.AimModifier.inProgress)
            {
                if (CheckDiagonalInput(dir))
                {
                    turnSystem.RequestPlayerRotate(dir);
                    timeFromLastRot = Time.time;
                }
                else if (CheckDiagonalInput(lastMoveDir))
                {
                    turnSystem.RequestPlayerRotate(dir);
                    lastDiagonalMoveDir = lastMoveDir;
                    timeFromLastRot = Time.time;
                }
                else
                {
                    turnSystem.RequestPlayerRotate(dir);
                }
            }
            else
            {
                turnSystem.RequestPlayerMove(dir);
                Debug.Log($"Requested move with input: {input.ReadValue<Vector2>()}");
            }
            lastMoveDir = dir;
        }
    }

    bool CheckDiagonalInput(Vector2 dir)
    {
        float angle = Vector2.Angle(Vector2.right, dir);

        if (
            (angle > 30f && angle <= 60f) ||
            (angle > 120f && angle <= 150f) ||
            (angle > -150f && angle <= -120f) ||
            (angle > -60f && angle <= -30f)
            )
        {
            return true;
        }
        return false;
    }

    void StandardAttack(InputAction.CallbackContext input)
    {
        if (moveMode)
        {
            Debug.Log("Used standard attack");
            turnSystem.RequestPlayerStandardAttack();
            SetToAwaitMode();
        }
    }

    void DiagonalAimFilter(InputAction.CallbackContext input)
    {
        if (Time.time - timeFromLastRot < 0.025f)
        {
            turnSystem.RequestPlayerRotate(lastDiagonalMoveDir);
        }
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
        if (uiMode)
        {
            UIManager.instance.MoveCursor(input.ReadValue<Vector2>());
        }
    }

    void Select(InputAction.CallbackContext input)
    {
        if (uiMode)
        {
            UIManager.instance.SelectElement();
        }
    }

    void Back(InputAction.CallbackContext input)
    {
        if (uiMode)
        {
            UIManager.instance.Back();
        }
    }

    void OpenMenu(InputAction.CallbackContext input)
    {
        if (uiMode)
        {
            UIManager.instance.FullExit(true);
            SetToMoveMode();
        }
        else if (moveMode)
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
     * if (angle > -30f && angle <= 30f)
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
       
     */
}
