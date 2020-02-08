using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
  public NavMeshAgent agent; //enemy AI navmesh
  public Vector3[] patrolPoints;

  private int point = 0;

    // Start is called before the first frame update
    void Start()
    {
      agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
      Patrol();
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
}
