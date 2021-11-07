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
    public Checkpoint consultingRoomCheckpoint;
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

    }

    public void MoveAutisticToSeat()
    {
        autistic.FollowCheckpoint(seatCheckpoint);
    }

    public async void StartPlayingWithToy()
    {
        // Come�a a chorar ap�s 5 segundos
        await Task.Delay(TimeSpan.FromSeconds(5));
        autistic.StartCrying();
    }

    public async void OnChannelChange()
    {
        if (!changedToCartoon && videoPlayer.clip.Equals(cartoonClip))
        {
            changedToCartoon = true;
            autistic.StopCrying();
            autistic.DropLeftHandItem();
            // autistic.LookAt(tv);
            await Task.Delay(TimeSpan.FromSeconds(5));
            consultingRoomDoor.UnlockDoor();
            consultingRoomDoor.OpenDoor();
            // A doutora chama para o atendimento, mas a crian�a n�o quer sair
        }
        if (!turnedTvOff && cartoonClip == null && changedToCartoon)
        {
            turnedTvOff = true;
            autistic.GetUp();
            autistic.FollowCheckpointWithParent(consultingRoomCheckpoint);
        }
    }
}
