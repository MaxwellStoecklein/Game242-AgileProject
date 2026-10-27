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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrintName()
    {
        Debug.Log(GetComponentInChildren<TMP_Text>().text);
    }

    public void SendRequest()
    {
        GetComponentInParent<PanelControl>().ReceiveRequest(targetMenu, openBehavior, requestType);
    }
    
}
