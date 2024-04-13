using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Transform[] PatrolStations;
    public float speed;


    public enum NpcStates { Patrol, Attack}
    public NpcStates NpcCurrentStates = NpcStates.Patrol;

    int currentPatrolStation = 0;
    GameObject target = null;

    
    void Update()
    {
        switch(NpcCurrentStates) 
        {
            case NpcStates.Patrol:
                //next station
                transform.LookAt(new Vector3(PatrolStations[currentPatrolStation].position.x, transform.position.y, PatrolStations[currentPatrolStation].position.z));
                if(Vector3.Distance(PatrolStations[currentPatrolStation].position, transform.position) < 0.1f)
                {
                    currentPatrolStation = (currentPatrolStation + 1) % PatrolStations.Length;
                }

                transform.position += transform.forward.normalized * speed * Time.deltaTime;


                break;
            case NpcStates.Attack:
                //attack mode
                transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));

                //if(Vector3.Distance(target.transform.position, transform.position) > 12)
                //{
                //    target = null;
                //    NpcCurrentStates = NpcStates.Patrol;
                //}

                break;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Attack Mode Active");

            NpcCurrentStates = NpcStates.Attack;
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            target = null;
            NpcCurrentStates = NpcStates.Patrol;
        }
    }
}
