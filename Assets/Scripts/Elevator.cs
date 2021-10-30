using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorClose;

    private AnimationClip openAnim, closeAnim;
    private bool doorOpen = false;
    private Animation animationTransform;

    // Start is called before the first frame update
    void Start()
    {
        animationTransform = transform.GetComponent<Animation>();
        openAnim = animationTransform.GetClip("OpenDoorsV2");
        closeAnim = animationTransform.GetClip("CloseDoorsV2");
    }

    public void OpenDoor()
    {
        if (doorOpen) return;
        animationTransform.clip = openAnim;
        animationTransform.Play();
        doorOpen = true;
        OnDoorOpen.Invoke();
    }

    public void CloseDoor()
    {
        if (!doorOpen) return;
        animationTransform.clip = closeAnim;
        animationTransform.Play();
        doorOpen = false;
        OnDoorClose.Invoke();
    }
}
