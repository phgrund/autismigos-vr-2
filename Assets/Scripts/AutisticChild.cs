using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AutisticChild : MonoBehaviour
{
    public UnityEvent OnCheckpointReached;
    public float turnSpeed = 1f;

    public Transform leftHand;
    private Transform leftHandTransform;
    private GameObject leftHandHold = null;
    private Transform oldLeftHandParent;

    public Transform rightHand;
    private Transform rightHandTransform;
    private GameObject rightHandHold = null;
    private Transform oldRightHandParent;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isNavigating = false;
    private Checkpoint currentCheckpoint;

    int IsWalkingHash;

    void Awake()
    {
        IsWalkingHash = Animator.StringToHash("IsWalking");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        leftHandTransform = leftHand.Find("Attach") ?? leftHand.transform;
        rightHandTransform = rightHand.Find("Attach") ?? rightHand.transform;
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
        // Debug.Log(isNavigating);
    }

    public void FollowCheckpoint(Checkpoint checkpoint)
    {
        isNavigating = true;
        agent.SetDestination(checkpoint.transform.position);
        currentCheckpoint = checkpoint;
    }

    public void AttachObjectToLeftHand(GameObject obj)
    {
        DropLeftHandItem();
        oldLeftHandParent = obj.transform.parent;
        AttachObjectToHand(leftHandTransform, obj);
    }

    private void DropLeftHandItem()
    {
        if (leftHandHold == null) return;
        leftHandHold.transform.parent = oldLeftHandParent;
        oldLeftHandParent = null;
    }

    public void AttachObjectToRightHand(GameObject obj)
    {
        DropRightHandItem();
        oldRightHandParent = obj.transform.parent;
        AttachObjectToHand(rightHandTransform, obj);
    }

    private void DropRightHandItem()
    {
        if (rightHandHold == null) return;
        rightHandHold.transform.parent = oldRightHandParent;
        oldLeftHandParent = null;
    }

    private void AttachObjectToHand(Transform hand, GameObject obj)
    {
        Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        // Collider collider = obj.GetComponent<Collider>();
        // if (collider != null) collider.isTrigger = true;
        obj.transform.parent = hand;
        obj.transform.position = hand.transform.position;
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
