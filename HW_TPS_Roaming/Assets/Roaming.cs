using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roaming : MonoBehaviour
{
    public Transform wayPointsRoot;    
    public int n = 0;
    public float roamingSpeed = 2f;

    NavMeshAgent agent;    
    List<Transform> wayPoints;
    bool isRoaming = false;
    public bool isChasing;
    Animator anim;     

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wayPoints = new List<Transform>();
        anim = GetComponent<Animator>();
        //isChasing = anim.GetBehaviour<MobChaseNavMesh>().isChasing;
        agent.speed = roamingSpeed;
        
        foreach(Transform t in wayPointsRoot)
        {
            wayPoints.Add(t);
        }        

        MoveWayPoint();        
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing)
        {
            isRoaming = false;            
            anim.SetBool("isRoaming", isRoaming);
        }
        if(!isChasing)
        {
            isRoaming = true;
            anim.SetBool("isRoaming", isRoaming);            
        }
        if (agent.remainingDistance <= 1.5f && isRoaming)
        {
            n++;
            n %= wayPoints.Count;            
            anim.SetBool("isRoaming", isRoaming);
            MoveWayPoint();
        }
    }

    public void MoveWayPoint()
    {
        agent.isStopped = false;
        isRoaming = true;
        anim.SetBool("isRoaming", isRoaming);

        agent.destination = wayPoints[n].transform.position;        
    }
}
