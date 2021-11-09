using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberDropArea : MonoBehaviour
{
    public AutisticChild autistic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Number") && !autistic.GetCurrentHandItem())
        {
            Debug.Log("Touch number");
            autistic.PickItemUp(other.gameObject);
        }
    }
}
