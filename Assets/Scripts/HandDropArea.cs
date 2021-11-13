using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandDropArea : MonoBehaviour
{
    public AutisticChild autistic;
    public Checkpoint test;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hand Collision");
        if (!autistic.GetCurrentHandItem() && other.gameObject.TryGetComponent(out XRGrabInteractable grabbable))
        {
            Debug.Log("Attach grabbable to hand");
            autistic.AttachObjectToRightHand(grabbable.gameObject);
            autistic.OnItemPickUp.Invoke();
        }
    }
}
