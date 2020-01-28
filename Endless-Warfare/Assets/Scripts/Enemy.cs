using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;


public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int maxHp;
    public int currHp;
    public int scoreToGive;

    [Header("Movement")]
    public float attackRange;
    public float moveSpeed;
    public float yPathOffset;

    private List<Vector3> path;

    private Turret turret;
    private GameObject target;

    private void Start()
    {
        // get the components
        turret = GetComponentInChildren<Turret>();
        target = FindObjectOfType<PlayerController>().gameObject;

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if (dist <= attackRange)
        {
            if (turret.CanShoot())
            {
                turret.Shoot();
            }
        }
        else
        {
            ChaseTarget();
        }

        //look at the target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;
    }

    void ChaseTarget()
    {
        if (path.Count == 0)
            return;
        //move toward the closest path
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);

        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
        {
            path.RemoveAt(0);
        }
    }

    void UpdatePath()
    {
        // calculate a path to the target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        // save that as a list
        path = navMeshPath.corners.ToList();
    }

    public void TakeDamage(int damage)
    {
        currHp -= damage;
        if (currHp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
