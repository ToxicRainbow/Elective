using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EdoAI : MonoBehaviour
{
    public Transform target;

    void follow()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
    }


    private void Update()
    {
        follow();
    }
}
