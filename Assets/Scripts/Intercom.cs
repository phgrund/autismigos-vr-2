using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Intercom : MonoBehaviour
{
    public UnityEvent OnIntercomPress;
    private bool hasTouched = false;

    public void touchIntercom ()
    {
        Debug.Log("touched");
        if (hasTouched) return;
        hasTouched = true;
        OnIntercomPress.Invoke();
    }
}
