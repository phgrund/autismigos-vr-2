using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent OnDoorLock;
    public UnityEvent OnDoorUnlock;
    private Rigidbody doorRigidbody;

    private Quaternion originalRotationValue;
    private bool isLocked = false;

    void Awake()
    {
        doorRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        originalRotationValue = transform.rotation;
    }

    void Update()
    {

    }

    public void LockDoor()
    {
        if (!isLocked) return;
        isLocked = true;
        doorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        OnDoorLock.Invoke();
        Debug.Log("Door Locked");
    }

    public IEnumerator LockDoorCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        LockDoor();
    }

    public void LockDoorWithDelay(float seconds)
    {
        StartCoroutine(LockDoorCoroutine(seconds));
    }

    public void UnlockDoor()
    {
        if (isLocked) return;
        isLocked = false;
        doorRigidbody.constraints = RigidbodyConstraints.None;
        OnDoorUnlock.Invoke();
        Debug.Log("Door Unlocked");
    }

    public void OpenDoor()
    {
        transform.Rotate(0, 90, 0);
    }

    /*
    public void CloseDoor()
    {
        transform.rotation = originalRotationValue;
    }
    */

    public IEnumerator UnlockDoorCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UnlockDoor();
    }

    public void UnlockDoorWithDelay(float seconds)
    {
        StartCoroutine(UnlockDoorCoroutine(seconds));
    }
}
