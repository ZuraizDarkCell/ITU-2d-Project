using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOldScript : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 2f;
    public Transform pointA;
    public Transform pointB;

    [Header("Player Detection")]
    public Transform player;
    public float detectRange = 5f;
    public float attackRange = 1.2f;

    [Header("Idle")]
    public float idleTime = 1.5f;

    [Header("Attack")]
    public float attackCooldown = 1.5f;

    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 target;
    private bool movingToB = true;

    public bool isIdle;
    private float idleTimer;
    private float attackTimer;

    private bool isAttacking;
    public Collider2D attackCollider;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        if (player == null) return;

        attackTimer -= Time.deltaTime;

        float dist = Vector2.Distance(transform.position, player.position);

        if (dist <= attackRange)
        {
            Attack();
        }
        else if (dist <= detectRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    // ---------------- PATROL ----------------
    void Patrol()
    {
        if (isAttacking) return;

        anim.SetFloat("Speed", 1);
        anim.SetBool("Grounded", true);
        MoveTowards(target);
        float dist = Vector2.Distance(transform.position, target);
        if (dist < 0.5f)
        {
            //isIdle = true;
            idleTimer = idleTime;

            target = movingToB ? pointA.position : pointB.position;
            movingToB = !movingToB;
            //anim.SetFloat("Speed", 0);
            //Invoke(nameof(StopIdle), idleTime);
        }
    }

    void StopIdle()
    {
        isIdle = false;
    }

    // ---------------- CHASE ----------------
    void ChasePlayer()
    {
        if (isAttacking) return;

        anim.SetFloat("Speed", 1);
        anim.SetBool("Grounded", true);
        Vector2 playerPos = player.position;

        // clamp inside patrol bounds
        float minX = Mathf.Min(pointA.position.x, pointB.position.x);
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x);

        playerPos.x = Mathf.Clamp(playerPos.x, minX, maxX);

        MoveTowards(playerPos);
    }

    // ---------------- ATTACK ----------------
    void Attack()
    {
        if (attackTimer > 0) return;

        isAttacking = true;

        rb.velocity = Vector2.zero;

        
        anim.SetTrigger("Attack");
        attackCollider.enabled = true;

        attackTimer = attackCooldown;
    }

    void EndAttack()
    {
        attackCollider.enabled = false;
        isAttacking = false;
    }

    // ---------------- MOVEMENT ----------------
    void MoveTowards(Vector2 targetPos)
    {
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flip sprite
        if (direction.x > 0)
            transform.localScale = new Vector3(2, 2, 2);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-2, 2, 2);
    }
}
