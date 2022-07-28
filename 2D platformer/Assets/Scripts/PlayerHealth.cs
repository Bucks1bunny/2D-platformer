using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, TakeDamage
{
    [SerializeField]
    private float repulsiveForce;
    [SerializeField]
    private Image[] hearts;
    private int health = 3;
    private Animator anim;
    private Rigidbody2D rb;

    public void OnTakeDamage(int _damage)
    {
        for (int i = 0; i < _damage; i++)
        {
            foreach (var heart in hearts)
            {
                if (heart.IsActive())
                {
                    heart.enabled = false;
                    health--;
                    anim.SetTrigger("Damaged");

                    break;
                }
            }
        }
        if (health == 0)
        {
            Die();
        }
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            var force = collision.transform.position - transform.position;
            force = force.normalized;
            rb.AddForce(force * repulsiveForce);

            OnTakeDamage(1);
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Dead");
        Destroy(gameObject, 0.5f);
    }
}
