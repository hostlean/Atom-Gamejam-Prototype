using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    BoxCollider2D col2d;

    [SerializeField] private bool onPlatform = false;

    private float horizontalMovement;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float raycastThreshhold;
    [SerializeField] private float extraHeight = 0.05f;

    Animator animator;

    public float MovementSpeed
    {
        get
        {
            return movementSpeed;
        }

        set
        {
            movementSpeed = value;
        }
    }

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }

        set
        {
            jumpForce = value;
        }
    }


    [SerializeField]
    private bool grounded;
    public LayerMask targetLayer;

    [SerializeField] private bool _isRunning;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Jump();
        grounded = isGrounded();
        HandleAnimaton();
    }

    void FixedUpdate()
    {
        Move();
    }


    private void HandleAnimaton()
    {
        animator.SetBool("isRunning", _isRunning);
        bool canFall = true;
        bool canJump = true;

        if(Mathf.Sign(rb.gravityScale) < 0)
        {
            canJump = false;
            canFall = false;
        }


        if(rb.velocity.y < 0 && !grounded)
        {
            animator.SetBool("isFalling", canFall);
            animator.SetBool("isJumping", !canJump);
        }
        else if(rb.velocity.y > 0 && !grounded)
        {
            animator.SetBool("isFalling", !canFall);
            animator.SetBool("isJumping", canJump);
        }
        else
        {
            animator.SetBool("isFalling", false);
            animator.SetBool("isJumping", false);
        }
    }

    void Move()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        _isRunning = horizontalMovement != 0;
        rb.velocity = new Vector2(horizontalMovement * Time.deltaTime * movementSpeed, rb.velocity.y);
        FlipSprite();
    }

    void Jump()
    {
        Vector2 jumpVector;

        if (Mathf.Sign(rb.gravityScale) < 0)
        {
            jumpVector = Vector2.down;
        }
        else jumpVector = Vector2.up;

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(jumpVector * jumpForce, ForceMode2D.Impulse);
            SFXChanger.Instance.PlayJumpSound();
        }

    }

    void FlipSprite()
    {
        if (horizontalMovement < 0)
        {
            spriteRenderer.flipX = true;
        }
        
        else if (horizontalMovement > 0)
        {
            spriteRenderer.flipX = false;
        }
    }


    private bool isGrounded()
    {
        Bounds bounds = col2d.bounds;
        Vector2 boundsMid = new Vector2(bounds.center.x, bounds.min.y + raycastThreshhold);
        Vector2 boundsLeft = new Vector2(bounds.min.x, bounds.min.y + raycastThreshhold);
        Vector2 boundsRight = new Vector2(bounds.max.x, bounds.min.y + raycastThreshhold);

        if (Mathf.Sign(rb.gravityScale) < 0)
        {
            extraHeight = Mathf.Abs(extraHeight) * -1;
        }
        else extraHeight = Mathf.Abs(extraHeight);


        Color rayColor;

        RaycastHit2D raycastHitMiddle = Physics2D.Raycast(boundsMid,
                               Vector2.down, extraHeight, targetLayer);

        RaycastHit2D raycastHitLeft = Physics2D.Raycast(boundsLeft,
                               Vector2.down, extraHeight, targetLayer);

        RaycastHit2D raycastHitRight = Physics2D.Raycast(boundsRight,
                       Vector2.down, extraHeight, targetLayer);


        if (raycastHitMiddle.collider != null || raycastHitLeft.collider != null || raycastHitRight.collider != null)
        {
            rayColor = Color.green;
        }
        else rayColor = Color.red;

        Debug.DrawRay(boundsMid,
                               Vector2.down * extraHeight, rayColor);
        Debug.DrawRay(boundsLeft,
                               Vector2.down * extraHeight, rayColor);
        Debug.DrawRay(boundsRight,
                               Vector2.down * extraHeight, rayColor);

        //if(raycastHitMiddle.collider.tag == "Platform" ||
        //    raycastHitLeft.collider.tag == "Platform" ||
        //    raycastHitRight.collider.tag == "Platform")
        //    onPlatform = true;
        //else onPlatform = false;

        //if(onPlatform)
        //    transform.parent = raycastHitMiddle.transform;
        //else transform.parent = null;

        return raycastHitMiddle.collider != null || raycastHitLeft.collider != null || raycastHitRight.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
            transform.SetParent(collision.collider.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
            gameObject.transform.parent = null;
    }

}
