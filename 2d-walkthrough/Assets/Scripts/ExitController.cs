using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExitController : MonoBehaviour
{
    public ExitArrivalEvent ExitArrivalEvent;

    void Start()
    {
        // Создать маркер??
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