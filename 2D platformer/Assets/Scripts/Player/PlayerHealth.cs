using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, TakeDamage
{
    public event Action Died = delegate { };

    [SerializeField]
    private float repulsiveForce;
    private Animator anim;
    private Rigidbody2D rb;

    public void OnTakeDamage()
    {
            Die();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    //Checked if enemy is mushroom or not
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            if (collision == collision.GetComponent<Mushroom>().upperBody)
            {
                var force = transform.position - collision.transform.position;
                force.Normalize();
                rb.AddForce(force * repulsiveForce);
            }
            else
            {
                OnTakeDamage();
            }
        }
    }

    private void Die()
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("Dead");
        Destroy(gameObject, 0.5f);
        Died();
    }
}
