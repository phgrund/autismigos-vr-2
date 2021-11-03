using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSystem : MonoBehaviour
{
    public static GameSystem Instance { get; private set; }
    public UnityEvent OnGameStart;
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one instance!");
            return;
        }

        Instance = this;
    }

    void Start()
    {
        OnGameStart.Invoke();
    }
}
