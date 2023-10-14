using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;

    //SerializeField allows private variables to be seen and changed in the inspector
    [SerializeField] private float speed = 10.5f;
    [SerializeField] private float rotationSpeed = 2;

    private float maxVelocity = 3;

    void Start()
    {
        //Get a reference to the attached RigidBody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //Get user inputs

        if (Input.GetKey(KeyCode.W))
        {
            ThrustUp();
        }

        if (Input.GetKey(KeyCode.S))
        {
            ThrustDown();
        }

        ClampVelocity();
    }

    //Clamp velocity according to max velocity. Prevents overacceleration.
    private void ClampVelocity()
    {
        float x = Mathf.Clamp(rb.velocity.x, -maxVelocity, maxVelocity);
        float y = Mathf.Clamp(rb.velocity.y, -maxVelocity, maxVelocity);

        rb.velocity = new Vector2(x, y);
    }

    //Apply upward force
    private void ThrustUp()
    {
        Vector2 force = transform.up * speed;
        rb.AddForce(force);
    }

    //Apply downward force
    private void ThrustDown()
    {
        Vector2 force = -transform.up * speed;
        rb.AddForce(force);
    }

    //Rotate the object
    private void Rotate(float amount)
    {
        transform.Rotate(0, 0, amount);
    }
}
