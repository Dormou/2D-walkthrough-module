using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitController : MonoBehaviour
{
    public string Label;

    public ExitArrivalEvent ExitArrivalEvent;

    void Start()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ExitArrivalEvent.Invoke(gameObject);
        }
    }
}

[System.Serializable]
public class ExitArrivalEvent : UnityEvent<GameObject> {}