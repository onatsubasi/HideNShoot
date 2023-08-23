using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSelectSampleActions : MonoBehaviour
{
    [SerializeField] GameObject objectToOpen;

    public void DestroySelfOpenObject()
    {
        if(objectToOpen != null)
            objectToOpen.SetActive(true);
        Destroy(gameObject);
    }
}
