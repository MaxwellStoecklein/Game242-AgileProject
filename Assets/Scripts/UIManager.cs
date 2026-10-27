using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Canvas canvas;

    public GameObject panelPrefab;
    public GameObject textPrefab;
    public GameObject discreetButtonPrefab;
    
    public Stack<MenuControl> menus = new Stack<MenuControl>();
    public MenuControl currentMenu;

    public GameObject cursor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //currentMenu = Instantiate(panelPrefab, canvas.transform).GetComponent<MenuControl>();
        MenuSetup(currentMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void MenuSetup(MenuControl menu)
    {
        menu.InitialSetup();
    }
    
    public void MoveCursor(Vector2 dir)
    {
        cursor.transform.position = currentMenu.MoveCursor(dir);
    }

    public void SelectElement()
    {
        currentMenu.SelectElement();
    }
    
    public void Back()
    {
        currentMenu.Back();
        currentMenu = menus.Pop();
    }

    public void FullExit()
    {
        foreach (MenuControl menu in menus)
        {
            Back();
        }
    }

    public void HideAllMenus()
    {
        Debug.Log("HideAllMenus not implemented");
    }
    
    public void SetCurrentMenu(MenuControl menu)
    {
        menus.Push(currentMenu);
        currentMenu = menu;
    }
}
