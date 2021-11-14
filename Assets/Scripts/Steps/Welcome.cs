using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class Welcome : MonoBehaviour
{
    public Door entranceDoor;
    public Checkpoint entranceDoorCheckpoint;
    public AutisticChild autistic;
    public InfoCanvas welcomeCanvas;

    void Start()
    {
        // Tranca a porta no início do jogo
        entranceDoor.LockDoor();
    }

    public async void ShowWelcomeMessage()
    {
        await Task.Delay(TimeSpan.FromSeconds(5));
        welcomeCanvas.gameObject.SetActive(true);
    }

    public void MoveAutisticToEntranceDoor ()
    {
        autistic.FollowCheckpoint(entranceDoorCheckpoint);
    }
}
