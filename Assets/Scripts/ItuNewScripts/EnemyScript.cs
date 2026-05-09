using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidbody2D;

    public Transform pointA;
    public Transform pointB;
    public bool movingToB = true;

    public Transform player;

    public float speed = 2f;    

    public float maxPlayerDistance = 5f;    
    public float attackRange = 1f;

    public float attackCooldown = 1f;
    public float lastAttackTime = 0f;
    public Collider2D attackCollider;

    public void SetAttackCollider(int x)
    {
        attackCollider.enabled = x == 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        lastAttackTime -= Time.deltaTime;
        lastAttackTime = Mathf.Max(lastAttackTime, 0);
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if(lastAttackTime <= 0.1f)
            {
                AttackPlayer();
            }    
        }
        else if(Vector2.Distance(transform.position, player.position) <= maxPlayerDistance)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    public void AttackPlayer()
    {
        lastAttackTime = attackCooldown;
        animator.SetTrigger("Attack");
    }

    public void ChasePlayer() 
    { 
       Vector2 direction = (player.position - transform.position).normalized;
       rigidbody2D.velocity = new Vector2(direction.x * speed, rigidbody2D.velocity.y);
       int speed_ = (int)Mathf.Abs(rigidbody2D.velocity.x);
       animator.SetInteger("Speed", speed_);
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }
    }

    public void Patrol()
    {
        transform.localScale = new Vector3(movingToB ? 2 : -2, 2, 2);
        if (movingToB)
        {
            rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
            animator.SetInteger("Speed", (int)Mathf.Abs(rigidbody2D.velocity.x));
            if(Vector2.Distance(transform.position, pointB.position) < 0.6f)
            {
                movingToB = false;
            }
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-speed, rigidbody2D.velocity.y);
            animator.SetInteger("Speed", (int)Mathf.Abs(rigidbody2D.velocity.x));
            if (Vector2.Distance(transform.position, pointA.position) < 0.6f)
            {
                movingToB = true;
            }
        }
    }
}
