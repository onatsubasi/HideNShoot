using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;


public enum HandSide
{
    Left,
    Right
}

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private PlayerGrab otherHand;
    [SerializeField] public  HandSide   handSide;

    public bool IsGrabbing => Player.IsVRPlayer ? Input.GetAxis(InputTagManager.GetGrip(handSide)) >= InputTagManager.VRInputThreshold
                                                 : Input.GetMouseButton(1);
    [HideInInspector] public GameObject carriedObject { get; private set; }


    private PCGrabSphereController pcGrabSphereController;
    private void Awake()
    {
        carriedObject = null;
        pcGrabSphereController = GetComponent<PCGrabSphereController>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(PlayerTagHolder.GrabbableTag)) // if tag isn't Grabbable, return
            return;

        // You can add your custom Grabs here if necessary via CustomTag checks like below

        //CustomTag customTag = other.GetComponent<CustomTag>();
        //if(customTag != null && customTag.HasTag(....) )
        //{
        //HandleYourCase();
        //}
        //else
        pcGrabSphereController?.SetMaterial(true);

        HandleGeneralCase(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(PlayerTagHolder.GrabbableTag)) // if tag isn't Grabbable, return
            return;
        pcGrabSphereController?.SetMaterial(false);
        //IGrabbable grabbable = null ;
        //if((grabbable = other.GetComponent<IGrabbable>()) != null && other.gameObject == carriedObject)
        //{
        //    grabbable.Release(this);
        //}
    }

    private void HandleGeneralCase(GameObject otherGameObject)
    {
        if (IsGrabbing && carriedObject == null)
        {
            carriedObject = otherGameObject;
            if (otherHand?.carriedObject == carriedObject)
                otherHand.carriedObject = null;

            otherGameObject.GetComponent<IGrabbable>()?.Grab(this);
        }
        else if (!IsGrabbing && carriedObject != null && otherGameObject == carriedObject)
        {
            carriedObject = null;
            otherGameObject.GetComponent<IGrabbable>()?.Release(this);
            pcGrabSphereController?.SetMaterial(false);
        }
    }
}
