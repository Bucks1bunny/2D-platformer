using UnityEngine;

public class Enemy : MonoBehaviour, TakeDamage
{
    [SerializeField]
    protected Transform[] points;
    [SerializeField]
    protected EnemyScriptableObject data;
    [SerializeField]
    private bool isWalking;
    [SerializeField]
    private GameObject coin;
    protected Transform target;
    protected Animator anim;
    private int waypointIndex = 0;
    private int direction = 1;
    private int health;

    public void OnTakeDamage(int _damage)
    {
        health -= _damage;
        anim.SetTrigger("Damaged");
        if (health == 0)
        {
            Death();
        }
    }

    protected virtual void Walk()
    {
        if (isWalking)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * data.speed * Time.deltaTime, Space.World);

            anim.SetBool("isMoving", true);
            if (Vector3.Distance(target.position, transform.position) <= 0.2f)
            {
                GetNextWaypoint();
            }
        }
    }

    protected void StopWalking()
    {
        transform.Translate(Vector3.zero, Space.World);
        anim.SetBool("isMoving", false);
    }

    private void Awake()
    {
        target = points[0];
        anim = GetComponent<Animator>();
        health = data.health;
    }

    private void Update()
    {
        Walk();
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex == 1)
        {
            waypointIndex = 0;
        }
        else
        {
            waypointIndex++;
        }
        Flip();
        target = points[waypointIndex];
    }

    private void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }

    private void Death()
    {
        anim.SetTrigger("Dead");
        Destroy(gameObject, 0.1f);
        Instantiate(coin, transform.position, Quaternion.identity);
    }
}
