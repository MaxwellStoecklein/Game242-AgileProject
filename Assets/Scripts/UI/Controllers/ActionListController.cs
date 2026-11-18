using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionListController : MonoBehaviour
{
    bool started = false;
    int readyCount = 0;
    
    List<DiscreetButtonControl> elements = new List<DiscreetButtonControl>(); // TODO: Turn into dict with <Ctrk, Action> pair
    
    private void OnEnable()
    {
        Debug.Log("ActionListController OnEnable");

        if (!started)
        {
            elements.AddRange(gameObject.GetComponentsInChildren<DiscreetButtonControl>());
            started = true;
        }
        
        Debug.Log("Started bool true");
        // Get current character's list of actions
            
        foreach (DiscreetButtonControl ctrl in elements)
        {
            InitializeElement(ctrl);
        }


        List<string> currentActions = TurnSystem.instance.GetCurrentActor().GetActions();
        int index = 0;

        foreach (string actionName in currentActions)
        {
            if (index < elements.Count)
            {
                elements[index].actionName = actionName;
                elements[index].ParseAction();
                index++;
            }
        }

        for (; index < elements.Count; index++)
        {
            elements[index].GetComponentInChildren<TMP_Text>().text = "---";
        }
        
        /*// This section is a placeholder to generate hardcoded actions since we have no stats yet
        DiscreetButtonControl btn;
        
        elements[0].GetComponentInChildren<TMP_Text>().text = "Cut (Functional)";
        btn = elements[0];
        // btn.targetMenu = generic action submenu
        btn.openBehavior = DiscreetButtonControl.OpenBehavior.AddWindow;
        //btn.requestType = DiscreetButtonControl.RequestType.OpenMenu;
        btn.requestType = DiscreetButtonControl.RequestType.Action; // TODO: Delete this once submenu is made.
        //btn.action = the action
        
        elements[1].GetComponentInChildren<TMP_Text>().text = "Bash";
        elements[2].GetComponentInChildren<TMP_Text>().text = "Focus";
            
        //GetComponent<PanelControl>().InitialSetup();
        
        if (started)
        {
            

        }*/
    }

    IEnumerator WaitForChildrenReady()
    {
        while (readyCount < elements.Count)
        {
            yield return null;
        }
        gameObject.SetActive(false);
    }

    public void ChildReady()
    {
        readyCount++;
    }

    private void OnDisable()
    {
        // execute these things when disabled.
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*elements.AddRange(gameObject.GetComponentsInChildren<DiscreetButtonControl>());
        
        started = true;
        StartCoroutine(WaitForChildrenReady());*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeElement(DiscreetButtonControl element) // TODO: Add Action as input param
    {
        element.gameObject.GetComponentInChildren<TMP_Text>().text = "ActionName";
    }
}
