using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class HandAnimation : MonoBehaviour
{
    [SerializeField] HandSide handSide;
    [SerializeField] Animator anim;

    private void Update()
    {
        anim.SetFloat("Grab",  Input.GetAxis(InputTagManager.GetGrip( handSide)));
        anim.SetFloat("Trigger", Input.GetAxis(InputTagManager.GetIndex(handSide)));
    }
}
