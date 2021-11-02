using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitingForAppointment : MonoBehaviour
{
    public Checkpoint entranceDoorCheckpoint;
    public Checkpoint seatCheckpoint;
    public AutisticChild autistic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveAutisticToSeat()
    {
        autistic.FollowCheckpoint(seatCheckpoint);
    }
}
