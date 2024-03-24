using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour//, TakeDamage
{
    [SerializeField]
    protected EnemyScriptableObject data;
    protected Vector3 target;
    protected Animator anim;

    [SerializeField]
    private bool isWalking;
    private List<Vector3> waypoints = new List<Vector3>();
    private int waypointIndex = 0;
    private int direction = 1;

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
}
