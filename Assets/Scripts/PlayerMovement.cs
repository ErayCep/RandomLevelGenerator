using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public LayerMask ground;

    //Jump Variables
    public float jumpForce = 10f;
    public float checkRadius = 0.3f;
    public Transform groundCheck;
    bool isGrounded;

    //Movement Variables
    public float moveSpeed = 10f;
    Vector2 moveInput;
    private bool facingRight = true;

    //Shoot Variables
    private float hookLife = 1f;
    private bool hookUsed = false;
    public Transform hookShot;
    public GameObject hook;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);

        moveInput.x = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(moveSpeed * moveInput.x, rb.velocity.y);

        if(facingRight == true && moveInput.x < 0)
        {
            FlipSprite();
        }
        else if(facingRight == false && moveInput.x > 0)
        {
            FlipSprite();
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.J))
        {
            ShootHook();
        }
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void FlipSprite()
    {
        facingRight = !facingRight;
        Vector3 playerScale = transform.localScale;
        playerScale.x *= -1;
        transform.localScale = playerScale;
    }

    void ShootHook()
    {
        GameObject hookProjectile = Instantiate(hook, hookShot.position, Quaternion.identity) as GameObject;

        Destroy(hookProjectile, hookLife);
    }
}
