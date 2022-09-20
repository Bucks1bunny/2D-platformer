using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour, TakeDamage
{
    [SerializeField]
    protected EnemyScriptableObject data;
    protected Vector3 target;
    protected Animator anim;

    [SerializeField]
    private bool isWalking;
    [SerializeField]
    private GameObject coin;
    private List<Vector3> waypoints = new List<Vector3>();
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
            anim.SetBool("isMoving", true);

            Vector3 dir = target - transform.position;
            transform.Translate(dir.normalized * data.speed * Time.deltaTime, Space.World);

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
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints.Add(transform.GetChild(i).transform.position);
        }
        target = waypoints[0];
        anim = GetComponent<Animator>();
        health = data.health;
    }

    private void Update()
    {
        Walk();
    }

    private void GetNextWaypoint()
    {
        switch (waypointIndex)
        {
            case 1:
                waypointIndex = 0;
                break;
            default:
                waypointIndex++;
                break;
        }
        Flip();
        target = waypoints[waypointIndex];
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
