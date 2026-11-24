using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    
    public Canvas canvas;

    public GameObject panelPrefab;
    public GameObject textPrefab;
    public GameObject discreetButtonPrefab;
    public GameObject damageTextPrefab;
    
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
        //MenuSetup(currentMenu);
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
            if (menus.Count > 0)
            {
                currentMenu.Back();
                currentMenu = menus.Pop();
                MenuSetup(currentMenu);
            }
            else
            {
                currentMenu.Back();
                currentMenu = null;
                cursor.gameObject.SetActive(false);
                ControlManager.instance.SetToMoveMode();
                
            }
        }
    }

    public void FullExit(bool moveable)
    {
        while (menus.Count > 0)
        {
            Back();
        }
        
        currentMenu.Back();
        currentMenu = null;
        cursor.gameObject.SetActive(false);

        if (moveable)
        {
            ControlManager.instance.SetToMoveMode();
        }
    }

    public void OpenMenu()
    {
        if (currentMenu == null)
        {
            cursor.gameObject.SetActive(true);
            currentMenu = rootMenu;
            rootMenu.gameObject.SetActive(true);
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

    public void RequestDamageNumberUI(GameObject victim, int damage)
    {
        StartCoroutine(DamageNumberAnimation(victim, damage));
    }

    private IEnumerator DamageNumberAnimation(GameObject victim, int damage)
    {
        GameObject damageText = Instantiate(damageTextPrefab);
        damageText.transform.SetParent(canvas.transform);
        damageText.transform.position = Camera.main.WorldToScreenPoint(victim.transform.position);
        damageText.GetComponentInChildren<TMP_Text>().text = $"{-damage}";

        Vector3 textOrigin = damageText.transform.position;
        Vector3 textDestination = new Vector3(textOrigin.x, textOrigin.y + 100f, textOrigin.z);
        
        float timer = 0f;
        float endTime = 1f;

        while (timer < endTime)
        {
            damageText.transform.position = Vector3.Lerp(textOrigin, textDestination, timer * (1f / endTime));
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(damageText);
    }
}
