using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Doctor : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private bool isNavigating = false;
    private Checkpoint currentCheckpoint;

    private bool isWalking = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isNavigating)
        {
            isNavigating = agent.remainingDistance != agent.stoppingDistance;
            // Chegou ao destino
            if (!isNavigating)
            {
                currentCheckpoint.InvokeCheckpointReached();
                currentCheckpoint = null;
                isWalking = false;
            }
        }


        animator.SetBool("IsWalking", isWalking);
    }

    public void FollowCheckpoint(Checkpoint checkpoint)
    {
        Debug.Log("Follow Checkpoint");
        isNavigating = true;
        isWalking = true;
        agent.SetDestination(checkpoint.transform.position);
        currentCheckpoint = checkpoint;
    }

    public void TeleportTo(Vector3 position, Quaternion rotation)
    {
        transform.rotation = rotation;
        agent.Warp(position);
    }
}
