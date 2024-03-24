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
    private int jumpCounter;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        anim.SetFloat("velocityY", rb.velocity.y);

        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, -Vector2.up, 1f, layerMask);
        moveX = Input.GetAxis("Horizontal");

        //Checking if player grounded, if true jumpCounter sets to 1
        if (hitGround.collider != null)
        {
            jumpCounter = 0;
            anim.SetBool("Jump", false);
        }
        else if (hitGround.collider == null)
        {
            bool Jumped = anim.GetBool("Jump");
            if (!Jumped)
            {
                anim.SetBool("Jump", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter != MaxJumpCount)
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
        anim.SetBool("isMoving", moveX != 0);
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCounter++;
    }

    private void Flip()
    {
        if (moveX > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
