using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveResults();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene("TestMenu");
    }

    public void QuitModule()
    {
        Application.Quit();
    }

    private void SaveResults()
    {
        WayDescription.IsPathCompleted = false;
    }
}
