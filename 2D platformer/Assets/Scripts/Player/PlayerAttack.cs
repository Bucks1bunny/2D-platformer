using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private CapsuleCollider2D boxCollider;
    [SerializeField]
    private float range;
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private int damage;
    private float cooldownTimer;
    private Animator anim;
    private Enemy enemyHealth;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && cooldownTimer > attackCooldown)
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center + transform.right * transform.localScale.x * range,
            Vector3.right * transform.localScale.x, 1f);

        if (hit.collider != null && hit.collider.tag == "Enemy")
        {
            enemyHealth = hit.transform.GetComponent<Enemy>();
            enemyHealth.OnTakeDamage(damage);
        }

        cooldownTimer = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(new Ray(boxCollider.bounds.center + transform.right * transform.localScale.x * range, Vector3.right * transform.localScale.x));
    }
}
