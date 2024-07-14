using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState {idle, running, jumping, falling};   // enum special variable that only accepts these 4 values, each value is assigned an index

    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   // fetch only once
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");                   // - or + for negative or positive axis, Raw: stops immediately on key release
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);     // value of the y velocity already posessed by the rb

        if (Input.GetButtonDown("Jump") && IsGrounded())    // Down: executes only when key is pressed down, not held, uses package manager
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState() {

        MovementState state;

        if (dirX > 0f) {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f) {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f) {  // slight imprecision
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f) {
            state = MovementState.falling;
        }

        anim.SetInteger("state",(int)state);  // enum to int
    }

    private bool IsGrounded() {

        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);     // checks if we overlap jumpable ground

        // create a box around player with same shape as box collider
        // 0f : for the rotation, .1f : amount by which box is offset towards bottom
        // use to check overlap with ground
        // does not overlap with sideways terrain while falling

    }
}
