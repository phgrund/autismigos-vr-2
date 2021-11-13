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
    public GameObject childCryingCanvas;
    public GameObject doctorArrivedCanvas;
    public GameObject toy;

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

    public void PickToyUp()
    {
        autistic.PickItemUp(toy);
    }

    public async void OnToyPickUp()
    {
        if (autistic.GetCurrentHandItem().TryGetComponent(out Toy _toy))
        {
            await Task.Delay(TimeSpan.FromSeconds(1.5f));
            MoveAutisticToSeat();
        }
    }

    public void MoveAutisticToSeat()
    {
        autistic.FollowCheckpoint(seatCheckpoint);
    }

    public async void StartPlayingWithToy()
    {
        // Começa a chorar após 5 segundos
        await Task.Delay(TimeSpan.FromSeconds(5));
        autistic.StartCrying();
        childCryingCanvas.SetActive(true);
    }

    public async void OnChannelChange()
    {
        if (!changedToCartoon && videoPlayer.clip.Equals(cartoonClip))
        {
            changedToCartoon = true;
            autistic.StopCrying();
            // autistic.LookAt(tv);
            await Task.Delay(TimeSpan.FromSeconds(5));
            consultingRoomDoor.UnlockDoor();
            consultingRoomDoor.OpenDoor();
            // A doutora chama para o atendimento, mas a criança não quer sair
            doctorArrivedCanvas.SetActive(true);
        }
    }

    public void OnVideoStop()
    {
        if (!turnedTvOff && changedToCartoon)
        {
            turnedTvOff = true;
            autistic.GetUp();
            autistic.DropLeftHandItem();
            autistic.FollowCheckpointWithParent(consultingRoomCheckpoint);
        }
    }
}
