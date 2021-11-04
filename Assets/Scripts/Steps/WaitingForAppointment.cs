using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.AI;
using System;
using System.Threading.Tasks;

public class WaitingForAppointment : MonoBehaviour
{
    public Door consultingRoomDoor;
    public Checkpoint seatCheckpoint;
    public AutisticChild autistic;

    public GameObject tv;
    public VideoClip cartoonClip;
    private VideoPlayer videoPlayer;
    private bool changedToCartoon = false;
    private bool turnedTvOff = false;

    void Awake()
    {
        videoPlayer = tv.GetComponent<VideoPlayer>();
    }

    void Start()
    {
        consultingRoomDoor.LockDoor();
    }

    // Update is called once per frame
    void Update()
    {
        if (!changedToCartoon && cartoonClip.Equals(videoPlayer.clip))
        {
            changedToCartoon = true;
            autistic.StopCrying();
            autistic.LookAt(tv);
        }
        if (!turnedTvOff && cartoonClip == null)
        {
            turnedTvOff = true;
            // Abrir a porta do consultório e chamar criança
        }
    }

    public void MoveAutisticToSeat()
    {
        autistic.FollowCheckpoint(seatCheckpoint);
    }

    public async void StartPlayingWithToy()
    {
        // Começa a chorar após 5 segundos
        await Task.Delay(TimeSpan.FromSeconds(1));
        autistic.StartCrying();
    }
}
