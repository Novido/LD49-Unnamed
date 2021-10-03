using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{

    private Rigidbody2D rb;
    private ShipManager shipManager;
    public float thrust;
    private float rotationalThrust;
    public float currentVelocity;

    //public float freightWeight = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shipManager = GetComponent<ShipManager>();

        rb.mass += shipManager.FreightWeight;
        thrust = shipManager.Thrust;
        rotationalThrust = shipManager.RotationalThrust;

    }

    // Update is called once per frame
    void Update()
    {
        // Add thrust force
        if(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (shipManager.Fuel > 0)
            {
                // Add upward force
                rb.AddForce(transform.up * thrust);

                // Burn fuel. Should be based on weight
                shipManager.BurnFuel();
            }
        }

        // Add Rotational Force
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rb.AddTorque(rotationalThrust);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rb.AddTorque(-rotationalThrust);
        }

        // Get Speed
        currentVelocity = rb.velocity.magnitude;
        //Debug.Log("Speed " + rb.velocity.magnitude.ToString());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (currentVelocity > 10)
            {
                //Debug.Log("You Lose!");
            }
            else
            {
                //Debug.Log("Hit!");
            }

        }
    }
}
