using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MenuControl
{
    private List<DiscreetButtonControl> elements = new ();
    int elementIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InitialSetup()
    {
        if (elements.Count > 0)
        {
            elements.Clear();
            Debug.LogWarning($"WARNING: Elements of {gameObject.name} have been cleared!");
        }
        
        List<DiscreetButtonControl> tmp = GetComponentsInChildren<DiscreetButtonControl>().ToList();
        foreach (DiscreetButtonControl button in tmp)
            elements.Add(button);
        
        ToggleChildButtons(true);
    }

    public override Vector2 MoveCursor(Vector2 dir)
    {
        if (elementIndex + Mathf.RoundToInt(dir.y) < elements.Count && 
            elementIndex + Mathf.RoundToInt(dir.y) >= 0)
        {
            elementIndex += Mathf.RoundToInt(dir.y);
            Vector2 pos = elements[elementIndex].gameObject.transform.position;
            pos.x -= elements[elementIndex].gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
            return pos;
        }
        else
        {
            return Vector2.zero;
        }
    }

    public override void SelectElement()
    {
        elements[elementIndex].GetComponentInChildren<DiscreetButtonControl>().SendRequest();
    }

    public override void Back()
    {
        gameObject.SetActive(false);
    }
    
    

    public void ReceiveRequest(MenuControl targetMenu, DiscreetButtonControl.OpenBehavior openBehavior,
        DiscreetButtonControl.RequestType requestType)
    {
        switch (requestType)
        {
            case DiscreetButtonControl.RequestType.Action:
                // TODO: Receive Request class containing Action. Do what is in the Action class.
                Debug.Log("Requested action!");
                UIManager.instance.FullExit();
                break;
            case DiscreetButtonControl.RequestType.OpenMenu:
                if (openBehavior == DiscreetButtonControl.OpenBehavior.Standalone)
                {
                    // TODO: Not done.
                    UIManager.instance.HideAllMenus();
                }
                else
                {
                    ToggleChildButtons(false);
                    targetMenu.gameObject.SetActive(true);
                    UIManager.instance.SetCurrentMenu(targetMenu);
                }
                break;
        }
    }

    public void TestSetup()
    {
        
    }

    public void ToggleChildButtons(bool active)
    {
        List<Button> buttons = new List<Button>();
        buttons = GetComponentsInChildren<Button>().ToList();
        foreach (Button button in buttons)
            button.enabled = active;
    }
    
    
    
    private void PrintName()
    {
    }
}
