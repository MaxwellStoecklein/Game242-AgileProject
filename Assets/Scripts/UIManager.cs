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
    public MenuControl rootMenu;

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

    public void InitCursor()
    {
        if (currentMenu != null)
        {
            Debug.Log("InitCursor ran");
            //cursor.transform.position = position;
            MoveCursor(new Vector2(1f, 0f));
        }
    }
    
    public void MoveCursor(Vector2 dir)
    {
        if (currentMenu != null)
        {
            cursor.transform.position = currentMenu.MoveCursor(dir);
        }
    }

    public void SelectElement()
    {
        if (currentMenu != null)
        {
            currentMenu.SelectElement();
        }
    }
    
    public void Back()
    {
        if (currentMenu != null)
        {
            currentMenu.Back();
            currentMenu = menus.Pop();
            MenuSetup(currentMenu);
        }
    }

    public void FullExit()
    {
        while (menus.Count > 0)
        {
            Back();
        }
        
        currentMenu.Back();
        currentMenu = null;
    }

    public void OpenMenu()
    {
        if (currentMenu == null)
        {
            rootMenu.gameObject.SetActive(true);
            currentMenu = rootMenu;
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
