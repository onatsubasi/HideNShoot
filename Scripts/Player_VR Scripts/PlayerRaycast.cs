using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycast : MonoBehaviour
{
    [SerializeField] HandSide handSide;
    [SerializeField] float maxRayDistance;
    [SerializeField] Material wrongMat, correctMat;
    [SerializeField] GameObject hitVisualizingSphere;

    [Range(0, 10)] private const float OutlineWidth = 5.0f;

    private LineRenderer lineRenderer;
    private MeshRenderer sphereMeshRenderer;

    private bool handLaserNoTarget;
    private GameObject previousHitGameObject;

    private GameObject clickedObject;
    

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        sphereMeshRenderer = hitVisualizingSphere.GetComponent<MeshRenderer>();

        clickedObject = null;
        lineRenderer.positionCount = 2;
        lineRenderer.material = wrongMat;
        handLaserNoTarget = true;
        
    }

    private void OnEnable()
    {
        clickedObject = null;
        handLaserNoTarget = true;
        lineRenderer.positionCount = 2;
        SetMaterials(false);
    }

    private void OnDisable()
    {
        clickedObject = null;
        lineRenderer.positionCount = 0;
        hitVisualizingSphere.SetActive(false);
        if(previousHitGameObject != null)
        {
            RemoveHoverEffects(previousHitGameObject);
            previousHitGameObject = null;
        }
        SetMaterials(false);
    }
    

    void Update()
    {

        float inputValue = Input.GetAxis(InputTagManager.GetIndex(handSide));
        bool isClicking = inputValue >= InputTagManager.VRInputThreshold || Input.GetMouseButtonDown(0);
        bool isReleasing = false;
        if(Player.IsVRPlayer)
        {
            isReleasing = inputValue < InputTagManager.VRInputThreshold;
        }
        else
        {
            isReleasing = Input.GetMouseButtonUp(0);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRayDistance))
        {
            lineRenderer.SetPosition(1, hit.point);
            SetSphereActive(true);
            hitVisualizingSphere.transform.position = hit.point;

            GameObject hitObject = hit.collider.gameObject;
            bool isSelectableTag = hitObject.CompareTag(PlayerTagHolder.SelectableTag);
            bool isGrabbableTag = hitObject.CompareTag(PlayerTagHolder.GrabbableTag);
            bool isSelectableOrGrabbable = isSelectableTag || isGrabbableTag;

            // Selectable object is hit, make line/sphere green and save newly hit object.
            if (isSelectableOrGrabbable && handLaserNoTarget )
            {
                SetMaterials(true);
                handLaserNoTarget = false;                  // laser targets an object
                previousHitGameObject = hitObject;                               // save the last gameobject
                ApplyHoverEffects(previousHitGameObject);       // open its corresponding outline

            }
            // An unselectable object is hit. Show red line/sphere. Unselect if previously a selectable object was selected
            else if ( !isSelectableOrGrabbable && !handLaserNoTarget) // an object is hit but not selectable
            {
                SetMaterials(false);
                handLaserNoTarget = true;                   

                if (previousHitGameObject != null)         
                {
                    RemoveHoverEffects(previousHitGameObject);
                    TryUnselect(previousHitGameObject);
                    previousHitGameObject = null;                                          
                }
            }
            // A selectable object is hit but it is different from the last hit selectable object. Unselect previous and save new one
            else if (isSelectableOrGrabbable && !handLaserNoTarget && previousHitGameObject != hitObject)
            {
                TryUnselect(previousHitGameObject);

                RemoveHoverEffects(previousHitGameObject);
                previousHitGameObject = hitObject;
                ApplyHoverEffects(hitObject);
            }


            if (isSelectableOrGrabbable &&  isClicking)
            {
                TrySelect(hitObject);
            }
            else if(isSelectableOrGrabbable && clickedObject != null && isReleasing)
            {
                TryUnselect(clickedObject);
            }
        }
        else // Raycast didn't hit anything. 
        {
            SetSphereActive(false);

            lineRenderer.SetPosition(1, transform.position + transform.forward * maxRayDistance);        // hand laser end point reset

            if (!handLaserNoTarget)                      // if there is no target in this frame
            {
                SetMaterials(false);
                handLaserNoTarget = true;                   // laser targets no object

                TryUnselect(previousHitGameObject);

                RemoveHoverEffects(previousHitGameObject);
                previousHitGameObject = null;                                          // unsave object
            }
        }
    }


    private void LateUpdate()
    {
        lineRenderer.SetPosition(0, transform.position);
    }

    private void SetMaterials(bool isCorrectMat)
    {
        if(isCorrectMat)
        {
            lineRenderer.material = correctMat;
            sphereMeshRenderer.material = correctMat;
        }
        else
        {
            lineRenderer.material = wrongMat;
            sphereMeshRenderer.material = wrongMat;
        }
    }

    // this can be customized for different SelectEffects
    void ApplyHoverEffects(GameObject gameObject)
    {
        DrawOutline(gameObject, OutlineWidth);
    }

    private void RemoveHoverEffects(GameObject gameObject)
    {
        DrawOutline(gameObject, 0f);
    }


    private void DrawOutline(GameObject gameObject, float newOutlineWidth)
    {
        var outline = gameObject?.GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = newOutlineWidth;
        }
    }

    void SetSphereActive(bool isActive) // hit visualizing sphere
    {
        if (Player.IsPCPlayer)
            return;

        if ( (hitVisualizingSphere.activeSelf && !isActive) 
          || (!hitVisualizingSphere.activeSelf && isActive)  )
        {
            hitVisualizingSphere.SetActive(!hitVisualizingSphere.activeSelf);
        }
    }

    private void TrySelect(GameObject gameObject)
    {
        if( clickedObject == null ) // to click a new object, clicked object must be null because if there was a clicked object it should've been Unselected.
        {
            gameObject.GetComponent<ISelectable>()?.Select(this);
            clickedObject = gameObject;
        }
    }

    private void TryUnselect(GameObject gameObject)
    {
        if(gameObject == clickedObject && gameObject != null )
        {
            clickedObject.GetComponent<ISelectable>()?.Unselect(this);
            clickedObject = null;
        }
    }
}
