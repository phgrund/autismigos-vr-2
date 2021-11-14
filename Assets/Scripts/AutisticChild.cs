using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class AutisticChild : MonoBehaviour
{
    [Header("Setup Fields")]
    public UnityEvent OnCheckpointReached;
    public UnityEvent OnItemPickUp;
    public float turnSpeed = 1f;

    private GameObject playerCamera;
    private NavMeshAgent agent;
    private Animator animator;
    private PlayContinuousSound playContinuousSound;
    private PlayQuickSound playQuickSound;
    public XRSocketInteractor leftHandSocketInteractor;
    // public XRSocketInteractor rightHandSocketInteractor;
    public Checkpoint checkpointPrefab;

    private bool isNavigating = false;
    public Checkpoint currentCheckpoint;
    private bool waitForPlayer = false;

    [Header("Tears")]

    public ParticleSystem leftEyeCryingParticle;
    public ParticleSystem rightEyeCryingParticle;

    [Header("Hands")]
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

    public AudioClip cheeringSound;
    public AudioClip walkingSound;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        playContinuousSound = GetComponent<PlayContinuousSound>();
        playQuickSound = GetComponent<PlayQuickSound>();
    }

    void Start()
    {
        playerCamera = GameObject.FindWithTag("MainCamera");
        leftEyeCryingParticle.Stop();
        rightEyeCryingParticle.Stop();
        playContinuousSound.Stop();
        leftHandStatus = new HandStatus();
        rightHandStatus = new HandStatus();
        leftHandStatus.transform = leftHand.Find("Attach") ?? leftHand.transform;
        rightHandStatus.transform = rightHand.Find("Attach") ?? rightHand.transform;
    }

    void Update()
    {
        if (agent.pathPending) return;

        if (isNavigating)
        {
            // Debug.Log($"Remaining: {agent.remainingDistance}, Stopping: {agent.stoppingDistance}");
            isNavigating = agent.remainingDistance != agent.stoppingDistance;
            // Chegou ao destino
            if (!isNavigating)
            {
                currentCheckpoint.InvokeCheckpointReached();
                currentCheckpoint = null;
                isWalking = false;
                waitForPlayer = false;
                agent.ResetPath();
                playContinuousSound.Stop(); // Stops walking sound
                // isRunning = false;
            }
            // Ainda está navegando
            else
            {
                // Se é para esperar o acompanhante
                if (waitForPlayer)
                {
                    Vector3 vectorToTarget = playerCamera.transform.position - transform.position;
                    vectorToTarget.y = 0;
                    float distanceToPlayer = vectorToTarget.magnitude;
                    // Debug.Log($"Distance to player: {distanceToPlayer}");
                    // Debug.Log($"Distance to Player: {distanceToPlayer}");
                    bool isNear = distanceToPlayer <= 1f;
                    isWalking = isNear;
                    agent.isStopped = !isNear;
                }
            }
        }

        if (isWalking) playContinuousSound.Play(walkingSound);
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

    public void FollowCheckpointWithParent(Checkpoint checkpoint)
    {
        waitForPlayer = true;
        FollowCheckpoint(checkpoint);
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

    private Checkpoint FollowItem(Transform position)
    {
        Checkpoint checkpoint = Instantiate(checkpointPrefab, position);
        checkpoint.targetTag = "Autistic";
        checkpoint.eventEmitType = Checkpoint.EventEmitType.ManualEmit;
        FollowCheckpoint(checkpoint);

        return checkpoint;
    }

    private Checkpoint FollowItem(Checkpoint checkpoint)
    {
        FollowCheckpoint(checkpoint);

        return checkpoint;
    }

    public void PickItemUp(GameObject item)
    {
        Checkpoint checkpoint = FollowItem(item.transform);
        checkpoint.OnCheckpointReached.AddListener(async () => {
            Destroy(checkpoint.gameObject);
            animator.SetTrigger("Lift");
            await Task.Delay(TimeSpan.FromSeconds(2f));
            AttachObjectToRightHand(item);
            OnItemPickUp.Invoke();
        });
    }

    public void PutItemDown(Checkpoint checkpoint)
    {
        FollowItem(checkpoint);
        checkpoint.OnCheckpointReached.AddListener(async () => {
            animator.SetTrigger("Lift");
            await Task.Delay(TimeSpan.FromSeconds(1.5f));
            DropRightHandItem();
        });
    }

    public GameObject GetCurrentHandItem()
    {
        return rightHandStatus.holding;
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
        Debug.Log($"Autistic is holding {handStatus.holding}");
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

    public void Cheer()
    {
        animator.SetTrigger("Cheer");
        playQuickSound.Play(cheeringSound);
    }

    public void TeleportTo(Vector3 position, Quaternion rotation)
    {
        transform.rotation = rotation;
        agent.Warp(position);
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
