using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f, jumpForce = 15f;

    public Transform orientation;

    float x, z;

    Vector3 moveDir;
    Vector3 slopeMoveDir;

    Rigidbody rb;

    public float groundDrag = 10f, airDrag = 2f;

    public float groundMoveMult = 10, airMoveMult = 0.4f;

    bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    RaycastHit slopeHit;

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        MyInput();
        ControlDrag();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if(OnSlope())
        {
            rb.useGravity = false;
        } 

        else
        {
            rb.useGravity = true;
        }

        slopeMoveDir = Vector3.ProjectOnPlane(moveDir, slopeHit.normal);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MyInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        moveDir = orientation.forward * z + orientation.right * x;
        moveDir.Normalize();
        slopeMoveDir.Normalize();
    }

    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDir * speed * groundMoveMult, ForceMode.Acceleration);
        }

        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDir * speed * groundMoveMult, ForceMode.Acceleration);
        }

        else if (!isGrounded)
        {
            rb.AddForce(moveDir * speed * airMoveMult, ForceMode.Acceleration);
        }
    }

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }

        else
        {
            rb.drag = airDrag;
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
}
