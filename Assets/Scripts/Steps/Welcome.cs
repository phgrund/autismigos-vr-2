using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Welcome : MonoBehaviour
{
    public Door entranceDoor;
    public Checkpoint entranceDoorCheckpoint;
    public AutisticChild autistic;

    void Start()
    {
        // Tranca a porta no início do jogo
        entranceDoor.LockDoor();
    }

    public void MoveAutisticToEntranceDoor ()
    {
        Debug.Log("move autistic");
        autistic.checkpoint = entranceDoorCheckpoint;
    }
}
