using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AutisticChild : MonoBehaviour
{
    public UnityEvent OnCheckpointReached;
    public float turnSpeed = 1f;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isNavigating = false;
    private Checkpoint currentCheckpoint;

    int IsWalkingHash;

    void Awake()
    {
        IsWalkingHash = Animator.StringToHash("IsWalking");
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Debug.Log(isNavigating);
        // Debug.Log($"{agent.remainingDistance} {agent.stoppingDistance}");
        if (isNavigating)
        {
            isNavigating = agent.remainingDistance != agent.stoppingDistance;
            if (!isNavigating)
            {
                // currentCheckpoint.
                currentCheckpoint = null;
            }
        }

        animator.SetBool(IsWalkingHash, isNavigating);
        Debug.Log(isNavigating);
    }

    public void FollowCheckpoint(Checkpoint checkpoint)
    {
        isNavigating = true;
        agent.SetDestination(checkpoint.transform.position);
        currentCheckpoint = checkpoint;
    }

    public void LookAt(GameObject other)
    {
        Quaternion _lookRotation = Quaternion.LookRotation((other.transform.position - transform.position).normalized);
        Debug.Log(_lookRotation);
        // transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
        transform.rotation = _lookRotation;
    }

    public void LookAtWithDelay(GameObject other)
    {
        StartCoroutine(LookAtCoroutine(other));
    }

    private IEnumerator LookAtCoroutine(GameObject other, float seconds = 3f)
    {
        yield return new WaitForSeconds(seconds);
        LookAt(other);
    }
}
