using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, TakeDamage
{
    [SerializeField]
    private Image[] hearts;
    [SerializeField]
    private Text coinText;
    private int coins;
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
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        {
            OnCoinPickup(1);
            Destroy(collision.gameObject);
        }
        else if (collision.transform.tag == "Enemy")
        {
            OnTakeDamage();
            if (health == 0)
            {
                Death();
            }
        }
    }

    private void OnCoinPickup(int amount)
    {
        coins += amount;
        coinText.text = coins.ToString();
    }

    private void Death()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Dead");
    }
}
