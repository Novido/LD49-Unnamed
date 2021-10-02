using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControl : MonoBehaviour
{

    private Rigidbody2D rb;
    public float thrust = 20f;
    public float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrust!");
            rb.AddForce(transform.up * thrust);
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
                Debug.Log("You Lose!");
            }
            else
            {
                Debug.Log("Hit!");
            }

        }
    }
}
