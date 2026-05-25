using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float movement;
    public Rigidbody2D rb;
    public Animator animator;
    public float speed = 5f;
    public float jumpHeight = 7f;
    public bool isGround = true;
    private bool facingRight = true;

    void Update()
    {
        movement = 0f;

        if (Keyboard.current.leftArrowKey.isPressed)
            movement = -1f;

        if (Keyboard.current.rightArrowKey.isPressed)
            movement = 1f;

        rb.linearVelocity = new Vector2(movement * speed, rb.linearVelocity.y);
        flip();
        if (Keyboard.current.spaceKey.isPressed && isGround)
        {
            Jump();
            animator.SetBool("Jump",true);
            isGround = false;
        }
        if(Mathf.Abs(movement) > .1f)
        {
            animator.SetFloat("Run", 1f);
        }
        else if (movement < .1f)
        {
            animator.SetFloat("Run", 0f);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpHeight);
    }
    void flip()
    {
        if (movement < 0f && facingRight)
        {
            transform.eulerAngles = new Vector3(0f,-180f,0f);
            facingRight = false;
        }
        else if (movement > 0f && facingRight == false)
        {
            transform.eulerAngles = new Vector3(0f,0f,0f);
            facingRight = true;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {   
        if (other.collider.CompareTag("Ground"))
            isGround = true;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Ground"))
            isGround = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
        {
            Destroy(other.gameObject);
        }
    }
}