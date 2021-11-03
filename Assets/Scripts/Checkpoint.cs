using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Checkpoint : MonoBehaviour
{
    public enum EventEmitType { CheckpointReached, ManualEmit }
    public EventEmitType eventEmitType = EventEmitType.CheckpointReached;

    public UnityEvent OnCheckpointReached;
    public bool firstReachOnly = true;
    public string targetTag;
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
        if (eventEmitType != EventEmitType.CheckpointReached) return;
        if (firstReachOnly && hasReached) return;
        if (other.gameObject.CompareTag(targetTag))
        {
            Debug.Log($"Checkpoint reached - {targetTag}");
            hasReached = true;
            OnCheckpointReached.Invoke();
        }
    }
}
