using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    public UnityEvent OnDoorOpen;
    public UnityEvent OnDoorOpenFinish;
    public UnityEvent OnDoorClose;
    public UnityEvent OnDoorCloseFinish;

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
        OnDoorOpen.Invoke();
        StartCoroutine(OpenDoorFinish());
    }

    private IEnumerator OpenDoorFinish()
    {
        yield return new WaitForSeconds(3f);
        doorOpen = true;
        OnDoorOpenFinish.Invoke();
    }

    public void CloseDoor()
    {
        if (!doorOpen) return;
        animationTransform.clip = closeAnim;
        animationTransform.Play();
        OnDoorClose.Invoke();
        StartCoroutine(CloseDoorFinish());
    }
    private IEnumerator CloseDoorFinish()
    {
        yield return new WaitForSeconds(3f);
        doorOpen = false;
        OnDoorCloseFinish.Invoke();
    }
}
