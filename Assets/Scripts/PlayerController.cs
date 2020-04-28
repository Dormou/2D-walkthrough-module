using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private int count;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
    }

    void FixedUpdate()
    {
       var moveHorizontal = Input.GetAxis("Horizontal");
       var moveVertical = Input.GetAxis("Vertical");

       var movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

       rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count++;
        }
    }
}
