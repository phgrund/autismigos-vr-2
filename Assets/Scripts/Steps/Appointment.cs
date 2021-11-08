using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Appointment : MonoBehaviour
{
    public AutisticChild autistic;
    public GameObject[] numbers;
    public Checkpoint numberWaitingCheckpoint;
    private int numberIndex = 0;
    private int numbersAmount;

    void Awake()
    {
        numbersAmount = numbers.Length;
        Debug.Log(numbersAmount);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public async void ReturnToNumberWaiting()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        autistic.FollowCheckpoint(numberWaitingCheckpoint);
    }
}
