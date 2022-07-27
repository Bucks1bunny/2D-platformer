using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected Transform[] points;
    [SerializeField]
    protected EnemyScriptableObject data;
    [SerializeField]
    private bool isWalking;
    protected Transform target;
    protected int waypointIndex = 0;
    protected Animator anim;
    private int direction = 1;

    private void Awake()
    {
        target = points[0];
        anim = GetComponent<Animator>();
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

    private void Flip()
    {
        direction *= -1;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, transform.localScale.z);
    }
}
