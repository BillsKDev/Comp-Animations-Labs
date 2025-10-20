using System;
using UnityEngine;
using UnityEngine.AI;

public class NavControl : MonoBehaviour
{
    [SerializeField] GameObject target;

    NavMeshAgent agent;
    Animator animator;
    bool isWalking = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isWalking)
        {
            agent.destination = target.transform.position;
        }
        else
            agent.destination = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Dragon")
        {
            isWalking = false;
            animator.SetTrigger("ATTACK");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Dragon")
        {
            isWalking = true;
            animator.SetTrigger("WALK");
        }
    }
}