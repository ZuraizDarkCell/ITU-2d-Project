using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public Collider2D groundCollider;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Collider2D attackCollider;
    private void Start()
    {
            rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        bool isGrounded = groundCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        animator.SetBool("Grounded", isGrounded);
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Flip(horizontalInput);
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        rigidbody2D.velocity = new Vector2(horizontalInput * 5, rigidbody2D.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 7f);
            animator.SetTrigger("Jump");
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");

        }

    }

    public void BoolSetAttackCollider(int isactive)
    {
        attackCollider.enabled = isactive == 1;
    }

    void Flip(float move)
    {
        if (move > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else if (move < 0)
            transform.localScale = new Vector3(-2, 2, 2);
    }
}
