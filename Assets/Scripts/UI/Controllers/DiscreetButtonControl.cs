using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Button))]
public class DiscreetButtonControl: MonoBehaviour
{
    public enum OpenBehavior
    {
        Standalone,
        AddWindow
    }

    public enum RequestType
    {
        Action,
        OpenMenu
    }
    
    public MenuControl targetMenu;
    public OpenBehavior openBehavior;
    public RequestType requestType;

    public string actionName;
    private IEntityAction parsedAction;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO
        ActionListController tmp = GetComponentInParent<ActionListController>();
        if (tmp != null)
        {
            Debug.Log("Ready");
            tmp.ChildReady();
        }
        ParseAction();

    }

    void ParseAction()
    {
        if (!string.IsNullOrEmpty(actionName))
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            parsedAction = assembly.CreateInstance(actionName) as IEntityAction;
            if (parsedAction != null)
            {
                Debug.LogWarning($"{gameObject.name} contains action: {parsedAction.GetType()}");
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} failed to parse action: {actionName}");
            }
        }
    }

    public IEntityAction GetAction()
    {
        return parsedAction;
    }
    
    public void PrintName()
    {
        Debug.Log(GetComponentInChildren<TMP_Text>().text);
    }

    public void SendRequest()
    {
        GetComponentInParent<PanelControl>().ReceiveRequest(targetMenu, this);
    }
    
}
