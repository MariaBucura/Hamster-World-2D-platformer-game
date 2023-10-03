using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private BoxCollider2D collider;
    private Animator animator;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX = 0f;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, walking, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {

        handleMovement();

        handleJump();
      
        UpdateAnimationState();

    }

    private void handleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        }
    }

    private void handleMovement()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rigidbody.velocity = new Vector2(dirX * moveSpeed, rigidbody.velocity.y);
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            spriteRenderer.flipX = false;
            state = MovementState.walking;
        }
        else if (dirX < 0f) 
        {
            spriteRenderer.flipX = true;
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rigidbody.velocity.y > .1f && !isGrounded())
        {
            state = MovementState.jumping;
        }
        else if (rigidbody.velocity.y < -.1f && !isGrounded())
        {
            state = MovementState.falling;
        }

        animator.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
