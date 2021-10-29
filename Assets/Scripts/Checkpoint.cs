using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent OnCheckpointReached;
    public bool firstReachOnly = true;
    private bool hasReached = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (firstReachOnly && hasReached) return;
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("reached");
            hasReached = true;
            OnCheckpointReached.Invoke();
        }
    }
}
