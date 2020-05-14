using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaypointController : MonoBehaviour
{
    public string Label;

    public WaypointArrivalEvent WaypointArrivalEvent;

    void Start()
    {
        gameObject.SetActive(false);
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
