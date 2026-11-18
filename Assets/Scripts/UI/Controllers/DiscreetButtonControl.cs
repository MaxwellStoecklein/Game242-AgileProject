using System;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
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
        //ParseAction();

    }

    /*private void OnEnable()
    {
        ParseAction();
    }*/

    public void ParseAction()
    {
        if (!string.IsNullOrEmpty(actionName))
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            parsedAction = assembly.CreateInstance(actionName) as IEntityAction;
            if (parsedAction != null)
            {
                Debug.LogWarning($"{gameObject.name} contains action: {parsedAction.GetType()}");
                GetComponentInChildren<TMP_Text>().text = parsedAction.GetName();
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} failed to parse action: {actionName}");
                GetComponentInChildren<TMP_Text>().text = "---";
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
