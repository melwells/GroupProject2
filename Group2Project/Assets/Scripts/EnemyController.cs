using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
  public NavMeshAgent agent; //enemy AI navmesh
  public Vector3[] patrolPoints;
  public float lookRadius = 10f;

  Transform target;

  private int point;

    // Start is called before the first frame update
    void Start()
    {
      target = PlayerManager.instance.player.transform;
      agent = GetComponent<NavMeshAgent>();
      point = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //Patrol();
      float distance = Vector3.Distance (target.position, transform.position);
      if (distance <= lookRadius)
      {
        agent.SetDestination(target.position);
        transform.LookAt(target.transform);
      }
      if (distance > lookRadius)
      {
        Patrol();
      }
    }

    //traverse NavMesh
    void Patrol()
    {
      gameObject.GetComponent<NavMeshAgent>().isStopped = false; //if not moving, move
      if (patrolPoints.Length > 0)
      {
        agent.SetDestination(patrolPoints[point]);
        if (transform.position == patrolPoints[point] || Vector3.Distance(transform.position, patrolPoints[point]) < 0.2f)
        {
          point++;
        }
        if (point >= patrolPoints.Length) //go back to start of patrol points
        {
          point = 0;
        }
      }

    }

    void OnDrawGizmosSelected() //debug purposes. See enemy's line of sight
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
