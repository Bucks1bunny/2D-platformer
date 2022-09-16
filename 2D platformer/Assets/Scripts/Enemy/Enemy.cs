using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, TakeDamage
{
    [SerializeField]
    protected List<Vector3> wayPoints = new List<Vector3>();
    [SerializeField]
    protected EnemyScriptableObject data;
    protected Vector3 target;
    protected Animator anim;

    [SerializeField]
    private bool isWalking;
    [SerializeField]
    private GameObject coin;
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
            Vector3 dir = target - transform.position;
            transform.Translate(dir.normalized * data.speed * Time.deltaTime, Space.World);

            anim.SetBool("isMoving", true);
            if (Vector3.Distance(target, transform.position) <= 0.2f)
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
        target = wayPoints[0];
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
        target = wayPoints[waypointIndex];
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
