
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float moveSpeedMultiplier;

    public float groundDrag;

    public float jumpForce;
    public float fallForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Animation")]
    public Animator playerAnim;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    private void Start()
    {   
        //exemple utilisation namespace
        //PlayerMovementTest.PlayerMovement.flag

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    private void Update()
    {
        //ground check
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z),0.2f, whatIsGround);
        MyInput();
        SpeedControl();
        Animation();

        //handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

    }
    /*private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 0.2f);
    }*/

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //when to jump
        if (Input.GetKey(jumpKey) && grounded) 
        {
            Debug.Log("saut");
            Jump();

            Invoke(nameof(Fall), jumpCooldown);
            
        }

        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            moveSpeedMultiplier = 3f;
        }
        else moveSpeedMultiplier = 1f;

    }

    private void Animation ()
    {
        //animation
        if (horizontalInput != 0 || verticalInput != 0)
            playerAnim.SetBool("Run", true);
        else
            playerAnim.SetBool("Run", false);

        playerAnim.SetBool("Jump", !grounded);
        playerAnim.SetFloat("CurrentSpeedMultiplier", moveSpeedMultiplier);

        //if (Input.GetKey(jumpKey) && grounded)
         //   playerAnim.SetBool("Jump", true);
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * moveSpeedMultiplier * 10f, ForceMode.Force);

        //in air
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

    }


    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
    {
        //reset y velocity
       // rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void Fall ()
    {
        rb.AddForce(-transform.up * fallForce , ForceMode.Impulse);
    }
}


