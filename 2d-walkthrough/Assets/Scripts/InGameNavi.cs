using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class InGameNavi : MonoBehaviour
{
    [SerializeField] private string PreviousScene;
    [SerializeField] private string NextScene;

    public void GoToPrevious()
    {
        SceneManager.LoadScene(PreviousScene);
    }

    public void GoToNext()
    {
        WayDescription.EndingTime = DateTime.Now;
        WayDescription.PathLength +=  WayDescription.CurrentPathPartLength;
        WayDescription.SaveResult();
        SceneManager.LoadScene(NextScene);
    }
}
