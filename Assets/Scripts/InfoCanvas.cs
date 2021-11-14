using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class InfoCanvas : MonoBehaviour
{
    public AudioClip canvasAudio;
    private PlayQuickSound playQuickSound;

    void Awake()
    {
        playQuickSound = GetComponent<PlayQuickSound>();
    }

    async void OnEnable()
    {
        await Task.Delay(TimeSpan.FromSeconds(1));
        if (canvasAudio != null) playQuickSound.Play(canvasAudio);
        Debug.Log("Info Canvas enabled");
    }
}
