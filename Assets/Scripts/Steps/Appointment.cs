using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Appointment : MonoBehaviour
{
    public AutisticChild autistic;
    public Doctor doctor;
    public GameObject[] numbers;
    public Checkpoint numberWaitingCheckpoint;
    public Checkpoint numberFinishedNumberSortingCheckpoint;
    public GameObject appointmentStartedCanvas;
    public FadeController fade;
    private int numbersPlaced = 0;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private async void SortNumberInPlace(NumberCube numberCube)
    {
        await Task.Delay(TimeSpan.FromSeconds(1.5));
        autistic.PutItemDown(numberCube.targetCheckpoint);
    }

    public void OnNumberPickUp()
    {
        if (autistic.GetCurrentHandItem().TryGetComponent(out NumberCube numberCube))
        {
            SortNumberInPlace(numberCube);
        }
    }

    public async void OnNumberPutDown()
    {
        numbersPlaced++;
        Debug.Log($"{numbersPlaced} out of {numbers.Length}");
        if (numbersPlaced == numbers.Length)
        {
            await Task.Delay(TimeSpan.FromSeconds(3f));
            autistic.FollowCheckpoint(numberFinishedNumberSortingCheckpoint);
        }
        else
        {
            await Task.Delay(TimeSpan.FromSeconds(3f));
            autistic.FollowCheckpoint(numberWaitingCheckpoint);
        }
    }

    public async void PutAutisticInGurney()
    {
        await Task.Delay(TimeSpan.FromSeconds(3f));
        fade.FadeOut();
        await Task.Delay(TimeSpan.FromSeconds(1f));

        autistic.TeleportTo(
            new Vector3(-8.1f, 1f, -1.9f),
            Quaternion.Euler(0f, 90f, 0f)
        );
        autistic.SitDown();

        doctor.TeleportTo(
            new Vector3(-7.09924746f, 0.0656685829f, -1.11507154f),
            Quaternion.Euler(0f, 240f, 0f)
        );

        fade.FadeOut();

        appointmentStartedCanvas.SetActive(true);
    }

    public void DoctorReachedGurney()
    {
        doctor.LookAt(new Vector3(0f, 90f, 0f));
    }
}
