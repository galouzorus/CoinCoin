using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMove : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    float Myfloat;
    
    // Update is called once per frame
    void Update()
    {
        // deplacement sur un plan
        InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {   //changement de position
        rb.MovePosition((Vector3)transform.position + InputKey * 10 * Time.deltaTime);


        //rotation
        float Angle = Mathf.Atan2(InputKey.x , InputKey.z) * Mathf.Rad2Deg;
        float Smooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, Angle, ref Myfloat, 0.1f);

        transform.rotation = Quaternion.Euler(0, Smooth, 0);

    }
}
