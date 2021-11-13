using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoCanvas : MonoBehaviour
{
    public AudioClip audio;
    private PlayQuickSound playQuickSound;

    void Awake()
    {
        playQuickSound = GetComponent<PlayQuickSound>();
    }

    void OnEnable()
    {
        if (audio != null) playQuickSound.Play(audio);
        Debug.Log("Info Canvas enabled");
    }
}
