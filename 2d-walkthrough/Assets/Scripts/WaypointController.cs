using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaypointController : MonoBehaviour
{
    // Строковый идентификатор объекта
    public string label;
    public WaypointArrivalEvent WaypointArrivalEvent;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            WaypointArrivalEvent.Invoke(gameObject);
        }
    }
}

[System.Serializable]
public class WaypointArrivalEvent : UnityEvent<GameObject> {}
