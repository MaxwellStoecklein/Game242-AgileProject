using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionListController : MonoBehaviour
{
    List<GameObject> elements = new List<GameObject>();
    
    private void OnEnable()
    {
        // TODO: Change to reuse the gameobjects instead of destroying.
        if (elements.Count > 0)
        {
            foreach (GameObject element in elements)
                Destroy(element);
            elements.Clear();
        }
        
        // Get current character's list of actions
        
        // This section is a placeholder to generate hardcoded actions since we have no stats yet
        DiscreetButtonControl btn;
        
        elements.Add(Instantiate(UIManager.instance.discreetButtonPrefab, transform));
        elements[0].GetComponentInChildren<TMP_Text>().text = "Cut (Functional)";
        btn = elements[0].GetComponent<DiscreetButtonControl>();
        // btn.targetMenu = generic action submenu
        btn.openBehavior = DiscreetButtonControl.OpenBehavior.AddWindow;
        //btn.requestType = DiscreetButtonControl.RequestType.OpenMenu;
        btn.requestType = DiscreetButtonControl.RequestType.Action; // TODO: Delete this once submenu is made.
        //btn.action = the action
        
        elements.Add(Instantiate(UIManager.instance.discreetButtonPrefab, transform));
        elements[1].GetComponentInChildren<TMP_Text>().text = "Bash";
        
        elements.Add(Instantiate(UIManager.instance.discreetButtonPrefab, transform));
        elements[2].GetComponentInChildren<TMP_Text>().text = "Focus";
        
        GetComponent<PanelControl>().InitialSetup();
    }

    private void OnDisable()
    {
        throw new NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
