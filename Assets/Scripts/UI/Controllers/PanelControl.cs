using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MenuControl
{
    private List<GameObject> elements = new List<GameObject>();
    
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
            elements.Add(button.gameObject);
    }

    public override Vector2 MoveCursor(Vector2 dir)
    {
        throw new System.NotImplementedException();
    }

    public override void SelectElement()
    {
        throw new System.NotImplementedException();
    }

    public override void Back()
    {
        throw new System.NotImplementedException();
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
        elements.Add(Instantiate(UIManager.instance.discreetButtonPrefab, gameObject.transform));
        elements[0].GetComponentInChildren<TMP_Text>().text = "Actions";
        
        elements.Add(Instantiate(UIManager.instance.discreetButtonPrefab, gameObject.transform));
        elements[1].GetComponentInChildren<TMP_Text>().text = "Spells";
        
        elements.Add(Instantiate(UIManager.instance.discreetButtonPrefab, gameObject.transform));
        elements[2].GetComponentInChildren<TMP_Text>().text = "Items";
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
