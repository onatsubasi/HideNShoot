using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicSelect : MonoBehaviour, ISelectable
{
    [SerializeField] UnityEvent selectEvent, unselectEvent;


    public void Select(PlayerRaycast playerRaycast)
    {
        selectEvent.Invoke();
        
    }

    public void Unselect(PlayerRaycast playerRaycast)
    {
        unselectEvent.Invoke();

    }
}
