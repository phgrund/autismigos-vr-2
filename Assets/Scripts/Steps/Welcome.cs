using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Welcome : MonoBehaviour
{
    public GameObject entranceDoor;
    public float entranceDoorSecondsDelay = 3f;
    public UnityEvent OnEntranceDoorLock;
    public UnityEvent OnEntranceDoorUnlock;

    void Start()
    {
        // Tranca a porta no início do jogo
        lockEntranceDoor();
    }

    private void changeEntranceDoorFreezingStatus(bool status)
    {
        entranceDoor.GetComponent<Rigidbody>().freezeRotation = status;
        if (status)
        {
            OnEntranceDoorLock.Invoke();
        }
        else
        {
            OnEntranceDoorUnlock.Invoke();
        }
    }

    private IEnumerator changeEntranceDoorFreezingStatusWithDelay(bool status)
    {
        yield return new WaitForSeconds(entranceDoorSecondsDelay);
        changeEntranceDoorFreezingStatus(status);
    }

    public void lockEntranceDoor()
    {
        changeEntranceDoorFreezingStatus(true);
    }

    public void unlockEntranceDoor()
    {
        changeEntranceDoorFreezingStatus(false);
    }

    public void lockEntranceDoorWithDelay()
    {
        StartCoroutine(changeEntranceDoorFreezingStatusWithDelay(true));
    }

    public void unlockEntranceDoorWithDelay()
    {
        StartCoroutine(changeEntranceDoorFreezingStatusWithDelay(false));
    }
}
