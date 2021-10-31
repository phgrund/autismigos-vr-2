using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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

    public void MoveAutisticToEntrance()
    {
        autistic.checkpoint = entranceDoorCheckpoint;
    }

    public async void MoveAutisticToSeat()
    {
        await Task.Delay(1);
        autistic.checkpoint = seatCheckpoint;
    }
}
