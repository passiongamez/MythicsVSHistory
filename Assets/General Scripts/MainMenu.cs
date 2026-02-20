using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject[] _menus;

    [SerializeField] GameObject[] _subMenus;

    private void Awake()
    {
        foreach(GameObject submenu in _subMenus)
        {
            submenu.SetActive(false);
        }

        foreach (GameObject menu in _menus)
        {
            menu.SetActive(false);
        }

        _menus[0].SetActive(true);
    }

    public void EnableMenu(int menuIndex)
    {
        foreach (GameObject submenu in _subMenus)
        {
            if (submenu.activeSelf)
                submenu.SetActive(false);
        }

        foreach (GameObject menu in _menus)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
        }

        _menus[menuIndex].SetActive(true);
    }

    public void EnableSubMenu(int subMenuIndex)
    {
        foreach (GameObject submenu in _subMenus)
        {
            if (submenu.activeSelf)
                submenu.SetActive(false);
        }

        foreach (GameObject menu in _menus)
        {
            if (menu.activeSelf)
                menu.SetActive(false);
        }

        _subMenus[subMenuIndex].SetActive(true);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}

