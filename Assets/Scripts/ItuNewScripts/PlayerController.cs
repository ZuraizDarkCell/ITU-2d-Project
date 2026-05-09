using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    public Collider2D groundCollider;
    public bool isGrounded; 
    public Collider2D attackCollider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetAttackCollider(int x)
    {
        attackCollider.enabled = x == 1;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = groundCollider.IsTouchingLayers(LayerMask.GetMask("Platform"));
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }
        rigidbody2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 5, rigidbody2D.velocity.y);//(1,0,-1)
        animator.SetInteger("Speed", (int)Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        if(Input.GetAxisRaw("Horizontal") > 0)//(1,0,-1)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,7f);
            animator.SetTrigger("Jump");
        }
        animator.SetBool("Grounded", isGrounded);
    }
}
