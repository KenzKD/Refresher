using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Player_RB;
    private BoxCollider2D Player_Collider;
    private SpriteRenderer Player_Sprite;
    private Animator anim;

    private float dirX = 0.0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { isIdle, isRunning, isJumping, isFalling };

    [SerializeField] private AudioSource jumpSFX;
    void Start()
    {
        Player_RB = GetComponent<Rigidbody2D>();
        Player_Collider = GetComponent<BoxCollider2D>();
        Player_Sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        Player_RB.velocity = new Vector2(dirX * moveSpeed, Player_RB.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSFX.Play();
            Player_RB.velocity = new Vector2(Player_RB.velocity.x, jumpForce);
        }
        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.isRunning;
            Player_Sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.isRunning;
            Player_Sprite.flipX = true;
        }
        else
        {
            state = MovementState.isIdle;
        }

        if (Player_RB.velocity.y > 0.1f)
        {
            state = MovementState.isJumping;
        }
        else if (Player_RB.velocity.y < -0.1f)
        {
            state = MovementState.isFalling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(Player_Collider.bounds.center, Player_Collider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
    }
}
