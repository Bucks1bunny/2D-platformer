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

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f, layerMask);
        moveX = Input.GetAxis("Horizontal");
        anim.SetFloat("velocityY", rb.velocity.y);

        if (hit.collider != null)
        {
            jumpCount = 1;
            anim.SetBool("Grounded", true);
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
        anim.SetBool("isMoving", moveX != 0);
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
            transform.localScale = Vector3.one;
        }
        else if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
