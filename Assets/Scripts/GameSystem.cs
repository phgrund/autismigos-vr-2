using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }
}
