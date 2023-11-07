using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class playerMove : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 InputKey;
    public bool isGrounded = false;
    private Camera cam;
    private Vector3 forward;
    private Vector3 right;
    public Transform camTarget;
    public int speed = 100;
    public int jump = 100;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {   
        //mouvement camera
        float mouseAxis = Input.GetAxis("Mouse X");
        camTarget.eulerAngles += new Vector3(0, mouseAxis * 10, 0);
       

        // deplacement sur un plan
        //InputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rotation
        //float Angle = Mathf.Atan2(InputKey.x, InputKey.z) * Mathf.Rad2Deg;
        //float Smooth = Mathf.SmoothDampAngle(camTarget.eulerAngles.y, Angle, ref Myfloat, 0.1f);

    }

    void FixedUpdate()

    {   //appel camera
        GetCamPosition();
        Vector3 relativeMoveDir = InputKey.x * right + InputKey.z * forward;

        //mouvement = vitesse + direction
        rb.velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetKey(KeyCode.Space))
        {
            //jump
            rb.velocity = new Vector3(0, 5, 0);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //run
            rb.velocity *= 5.0f;

         //changement de position
         //rb.MovePosition((Vector3)transform.position + relativeMoveDir * 10 * Time.deltaTime);

        }
    }

    void GetCamPosition()
    {
        forward = cam.transform.forward;
        right = cam.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
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


