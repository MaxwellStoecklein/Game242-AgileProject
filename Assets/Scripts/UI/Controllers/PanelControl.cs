using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelControl : MenuControl
{
    private List<DiscreetButtonControl> elements = new ();
    public int elementIndex = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //InitialSetup();
    }

    public override void InitialSetup()
    {
        if (elements.Count > 0)
        {
            elements.Clear();
            Debug.LogWarning($"WARNING: Elements of {gameObject.name} have been cleared!");
        }
        Debug.Log($"{gameObject.name} has been initialized!");
        
        ToggleChildButtons(true);
        List<DiscreetButtonControl> tmp = GetComponentsInChildren<DiscreetButtonControl>().ToList();
        foreach (DiscreetButtonControl button in tmp)
            elements.Add(button);
        Debug.Log($"Element count: {elements.Count}");
        Debug.Log($"My name: {gameObject.name}");
        Debug.Log($"Element 0 pos: {elements[0].gameObject.transform.position}");
        Debug.Log($"Element 0 name:  {elements[0].gameObject.name}");

        elementIndex = 0;
        
        /*Vector2 pos = elements[elementIndex].gameObject.transform.position;
        Debug.Log($"Position: {pos}");
        pos.x -= elements[elementIndex].gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
        Debug.Log($"Adjusted Position: {pos}");
        UIManager.instance.InitCursor(pos);*/
        UIManager.instance.InitCursor();
    }

    public override Vector2 MoveCursor(Vector2 dir)
    {
        Debug.Log($"My name: {gameObject.name}");
        Debug.Log($"Element 0 pos: {elements[0].gameObject.transform.position}");
        Debug.Log($"Element 0 name:  {elements[0].gameObject.name}");
        
        dir *= -1;
        if (elementIndex + Mathf.RoundToInt(dir.y) < elements.Count && 
            elementIndex + Mathf.RoundToInt(dir.y) >= 0)
        {
            elementIndex += Mathf.RoundToInt(dir.y);
            Vector2 pos = elements[elementIndex].gameObject.transform.position;
            Debug.Log($"Pos: {pos}");
            pos.x -= elements[elementIndex].gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
            return pos;
        }
        else if (elementIndex + Mathf.RoundToInt(dir.y) >= elements.Count)
        {
            elementIndex = 0;
            Vector2 pos = elements[elementIndex].gameObject.transform.position;
            Debug.Log($"Pos: {pos}");
            pos.x -= elements[elementIndex].gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
            return pos;
        }
        else if (elementIndex + Mathf.RoundToInt(dir.y) < 0)
        {
            elementIndex = elements.Count - 1;
            Vector2 pos = elements[elementIndex].gameObject.transform.position;
            Debug.Log($"Pos: {pos}");
            pos.x -= elements[elementIndex].gameObject.GetComponent<RectTransform>().sizeDelta.x / 2;
            return pos;
        }
        return Vector2.zero;
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
                    UIManager.instance.SetCurrentMenu(targetMenu);
                    targetMenu.gameObject.SetActive(true);
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
