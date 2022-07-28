using UnityEngine;

public class AttackingEnemy : Enemy
{
    [SerializeField]
    private float attackCooldown;
    [SerializeField]
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private float range;
    [SerializeField]
    private float colliderDistance;
    private PlayerHealth playerHealth;
    private float cooldownTimer = Mathf.Infinity;

    protected override void Walk()
    {
        if (PlayerInSight())
        {
            StopWalking();
        }
        else
        {
            base.Walk();
        }
    }

    private void Update()
    {
        Walk();

        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
                Attack();
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<PlayerHealth>();
        }
        return hit.collider != null;
    }

    private void Attack()
    {
        if (PlayerInSight())
        {
            playerHealth.OnTakeDamage(data.damage);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

}
