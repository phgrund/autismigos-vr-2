using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{
    public UnityEvent OnDoorLock;
    public UnityEvent OnDoorUnlock;
    private Rigidbody doorRigidbody;

    void Awake()
    {
        doorRigidbody = GetComponent<Rigidbody>();
    }

    public void LockDoor()
    {
        doorRigidbody.freezeRotation = true;
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
        doorRigidbody.freezeRotation = false;
        OnDoorUnlock.Invoke();
        Debug.Log("Door Unlocked");
    }

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
