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
    private AudioSource audioSource;
    private PlayContinuousSound playContinuousSound;

    private bool isNavigating = false;
    private Checkpoint currentCheckpoint;

    public ParticleSystem leftEyeCryingParticle;
    public ParticleSystem rightEyeCryingParticle;

    public Transform leftHand;
    public Transform rightHand;

    private HandStatus leftHandStatus;
    private HandStatus rightHandStatus;

    // Animation States
    private bool isWalking = false;
    // private bool isRunning = false;
    private bool isSitting = false;

    public AudioClip cryingSound;
    private bool isCrying = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playContinuousSound = GetComponent<PlayContinuousSound>();
    }

    void Start()
    {
        leftEyeCryingParticle.Stop();
        rightEyeCryingParticle.Stop();
        leftHandStatus = new HandStatus();
        rightHandStatus = new HandStatus();
        leftHandStatus.transform = leftHand.Find("Attach") ?? leftHand.transform;
        rightHandStatus.transform = rightHand.Find("Attach") ?? rightHand.transform;
    }

    void Update()
    {
        if (isNavigating)
        {
            isNavigating = agent.remainingDistance != agent.stoppingDistance && !agent.isStopped;
            if (!isNavigating)
            {
                currentCheckpoint.InvokeCheckpointReached();
                currentCheckpoint = null;
                isWalking = false;
                // isRunning = false;
            }
        }

        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsSitting", isSitting);
        // Debug.Log($"Is Sitting: {IsSitting}");
        // Debug.Log(isNavigating);
    }

    public void FollowCheckpoint(Checkpoint checkpoint)
    {
        isNavigating = true;
        isWalking = true;
        isSitting = false;
        agent.SetDestination(checkpoint.transform.position);
        currentCheckpoint = checkpoint;
    }

    public void SitDown()
    {
        if (currentCheckpoint) agent.isStopped = true;
        isWalking = false;
        // isRunning = false;
        isSitting = true;
    }

    public void GetUp()
    {
        if (!isSitting) return;
        isSitting = false;
    }

    public void StartCrying()
    {
        Debug.Log("Started Crying");
        if (isCrying) return;
        leftEyeCryingParticle.Play();
        rightEyeCryingParticle.Play();
        playContinuousSound.Play(cryingSound);
        isCrying = true;
    }

    public void StopCrying()
    {
        if (!isCrying) return;
        leftEyeCryingParticle.Stop();
        rightEyeCryingParticle.Stop();
        playContinuousSound.Stop();
        isCrying = false;
    }

    public void AttachObjectToLeftHand(GameObject obj)
    {
        DropHandItem(leftHandStatus);
        AttachObjectToHand(leftHandStatus, obj);
    }

    public void AttachObjectToRightHand(GameObject obj)
    {
        DropHandItem(rightHandStatus);
        AttachObjectToHand(rightHandStatus, obj);
    }

    public void DropLeftHandItem()
    {
        DropHandItem(leftHandStatus);
    }

    public void DropRightHandItem()
    {
        DropHandItem(rightHandStatus);
    }

    private void AttachObjectToHand(HandStatus handStatus, GameObject obj)
    {
        handStatus.rigidbody = obj.GetComponent<Rigidbody>();
        if (handStatus.rigidbody != null)
        {
            handStatus.oldConstraints = handStatus.rigidbody.constraints;
            handStatus.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        handStatus.holding = obj;
        handStatus.oldParent = obj.transform.parent;
        Debug.Log(handStatus.holding);
        obj.transform.parent = handStatus.transform;
        obj.transform.position = handStatus.transform.position;
    }

    private void DropHandItem(HandStatus handStatus)
    {
        if (handStatus.holding == null) return;
        handStatus.holding.transform.parent = handStatus.oldParent;
        handStatus.holding = null;
        handStatus.oldParent = null;
        if (handStatus.rigidbody) handStatus.rigidbody.constraints = handStatus.oldConstraints;
        handStatus.rigidbody = null;
    }

    // TODO: Ver o porquê não está funcionando
    public void LookAt(GameObject other)
    {
        Quaternion _lookRotation = Quaternion.LookRotation((other.transform.position - transform.position).normalized);
        // Debug.Log(_lookRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turnSpeed);
        // transform.rotation = _lookRotation;
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

class HandStatus
{
    public Transform transform { get; set; }
    public GameObject holding { get; set; }
    public Transform oldParent { get; set; }
    public Rigidbody rigidbody { get; set; }
    public RigidbodyConstraints oldConstraints { get; set; }
}
