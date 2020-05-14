using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private GameObject selectMenu;
    private GameObject mainMenu;

    void Start()
    {
        selectMenu = GameObject.Find("SelectMenu");
        mainMenu = GameObject.Find("MainMenu");

        SaveTestCase();
        selectMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(WayDescription.SpaceLabel);
    }

    public void BackFromSelectMenu()
    {
        SaveTestCase();
        selectMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void QuitModule()
    {
        Application.Quit();
    }

    private void SaveTestCase()
    {
        WayDescription.SpaceLabel = GameObject.Find("ShopGroup")?.GetComponent<ToggleGroup>()?.ActiveToggles()?.First().name;
        WayDescription.Waypoints = new HashSet<string>();
        var itemsList = GameObject.Find("ItemsList")?.GetComponentsInChildren<Toggle>();
        if(itemsList != null)
        {
            foreach (var item in itemsList)
            {
                if(item.isOn)
                {
                    var text = item.gameObject.GetComponentsInChildren<TextMeshProUGUI>()?.First().text;
                    WayDescription.Waypoints.Add(text);
                }
            }
        }
    }
}
