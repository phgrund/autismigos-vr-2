using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Welcome : MonoBehaviour
{
    public Door entranceDoor;

    void Start()
    {
        // Tranca a porta no in�cio do jogo
        entranceDoor.LockDoor();
    }
}
