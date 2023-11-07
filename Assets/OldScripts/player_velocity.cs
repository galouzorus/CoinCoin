using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_velocity : MonoBehaviour
{
    Rigidbody rb;
    public int speed = 5;
    public int jump = 5;
    public bool isGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get rigidbody component
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 mouvement = new Vector3(h, 0, v);
        
        rb.AddForce(transform.TransformDirection (mouvement) * speed * Time.deltaTime);
        transform.Rotate (Vector3.up * h * 50* Time.deltaTime);

        if (Input.GetKey(KeyCode.Space))
        {
            //jump
            rb.AddForce(Vector3.up * jump* Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //run
            rb.velocity *= 5.0f;

        }
        
    }
    void OnCollisionEnter()
    {
        isGrounded = true;
    }
    void OnCollisionExit()
    {
        isGrounded = false;
    }
}
