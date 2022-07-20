using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    private float moveX;
    private Rigidbody2D rb;
    private int MaxJumpCount = 2;
    private int jumpCount;
    private float scaleX;

    RaycastHit2D hit;

    private void Start()
    {
        scaleX = transform.localScale.x;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, layerMask);
        moveX = Input.GetAxis("Horizontal");

        if (hit.collider != null)
        {
            jumpCount = 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount != MaxJumpCount)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Flip();
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);
        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCount++;
    }

    private void Flip()
    {
        if (moveX > 0)
        {
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3((-1) * scaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
