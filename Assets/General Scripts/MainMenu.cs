using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] _menus;

    [SerializeField] GameObject[] _subMenus;

    Stack<MenuState> _menuHistory = new Stack<MenuState>();

    MenuState _previousMenu;

     struct MenuState
    {
        public bool isMainMenu;
        public int menuIndex;
    }

    private void Awake()
    {
        DisableMenus();

        _menus[0].SetActive(true);
        _menuHistory.Push(new MenuState { isMainMenu = true, menuIndex = 0 });
    }

    public void EnableMenu(int menuIndex)
    {
        if (menuIndex < 0 || menuIndex >= _menus.Length)
        {
            Debug.LogWarning("Invalid main menu index: " + menuIndex);
            return;
        }

        foreach (GameObject menu in _menus)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
        }

        _menus[menuIndex].SetActive(true);
        _menuHistory.Push(new MenuState { isMainMenu = true, menuIndex = menuIndex });

        foreach (GameObject submenu in _subMenus)
        {
            if (submenu.activeSelf)
                submenu.SetActive(false);
        }
    }

    public void EnableSubMenu(int subMenuIndex)
    {
        if (subMenuIndex < 0 || subMenuIndex >= _subMenus.Length)
        {
            Debug.LogWarning("Invalid sub menu index: " + subMenuIndex);
            return;
        }

        foreach (GameObject submenu in _subMenus)
        {
            if (submenu.activeSelf)
                submenu.SetActive(false);
        }

        _subMenus[subMenuIndex].SetActive(true);
        _menuHistory.Push(new MenuState { isMainMenu = false, menuIndex = subMenuIndex });

        foreach (GameObject menu in _menus)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
        }
    }

    void DisableMenus()
    {
        foreach (GameObject submenu in _subMenus)
        {
            submenu.SetActive(false);
        }

        foreach (GameObject menu in _menus)
        {
            menu.SetActive(false);
        }
    }

    public void Back()
    {
        if(_menuHistory.Count <= 1)
        {
            return;
        }

        _menuHistory.Pop();
        _previousMenu = _menuHistory.Peek();

        DisableMenus();

        if (_previousMenu.isMainMenu)
        {
            _menus[_previousMenu.menuIndex].SetActive(true);
        }
        else
        {
            _subMenus[_previousMenu.menuIndex].SetActive(true);
        }
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}

