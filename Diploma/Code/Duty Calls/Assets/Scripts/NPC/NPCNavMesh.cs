using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;
    private Transform walkPosition;
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (walkPosition != null)
        //{
        //    agent.SetDestination(walkPosition.position);
        //}

    }
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponent<TargetScript>();
        if(target != null)
        {
            walkPosition = other.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        walkPosition = null;
    }
    private void OnTriggerStay(Collider other)
    {
        var target = other.GetComponent<TargetScript>();
        if (target != null)
        {
            agent.SetDestination(other.transform.position);
        }
    }
}
