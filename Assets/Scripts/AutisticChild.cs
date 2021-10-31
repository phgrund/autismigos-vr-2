using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutisticChild : MonoBehaviour
{
    public UnityEvent OnCheckpointReached;
    public Checkpoint checkpoint { get; set; } = null;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoint) FollowCheckpoint();
    }

    public void FollowCheckpoint()
    {
        Vector3 dir = checkpoint.transform.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, checkpoint.transform.position) <= .2f) ClearCheckpoint();
    }

    public void ClearCheckpoint ()
    {
        checkpoint = null;
        OnCheckpointReached.Invoke();
    }
}
