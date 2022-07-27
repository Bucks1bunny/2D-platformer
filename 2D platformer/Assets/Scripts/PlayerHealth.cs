using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth
    : MonoBehaviour, TakeDamage
{
    [SerializeField]
    private Image[] hearts;
    private int health = 3;
    private Animator anim;
    private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnTakeDamage()
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
        if (health == 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            OnTakeDamage();
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Dead");
        Destroy(gameObject, 0.35f);
    }
}
