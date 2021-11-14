using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Intercom : MonoBehaviour
{
    public UnityEvent OnIntercomPress;
    private bool hasTouched = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("VR Hand"))
        {
            TouchIntercom();
        }
    }

    public void TouchIntercom ()
    {
        if (hasTouched) return;
        hasTouched = true;
        OnIntercomPress.Invoke();
    }
}
