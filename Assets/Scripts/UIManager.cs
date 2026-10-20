using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;

    public GameObject panelPrefab;
    public GameObject textPrefab;
    public GameObject discreetButtonPrefab;
    
    public Stack<MenuControl> menus = new Stack<MenuControl>();
    public MenuControl currentMenu;

    public GameObject cursor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentMenu = Instantiate(panelPrefab, canvas.transform).GetComponent<MenuControl>();
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
    }
}
