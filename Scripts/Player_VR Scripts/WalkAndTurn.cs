using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



[DisallowMultipleComponent]
public class WalkAndTurn : MonoBehaviour
{
    public float WalkSpeed;
    public float TurnSpeed;
    [HideInInspector] public bool isWalking;
    [HideInInspector] public bool isClimbing;
    [SerializeField] GameObject _camera;

    private void Awake()
    {
        isClimbing = false;
    }
    private void Update()
    {
        float hori, vert;
        //getting horizontal axis with max abs input;

        if (!isClimbing)
        {
            //getting vertical axis with max abs input;
            float vert1 =  Input.GetAxis(InputTagManager.GetThumbVert(HandSide.Left));
            float vert2 = -Input.GetAxis(InputTagManager.GetThumbVert(HandSide.Right)); ;
            float vert3 =  Input.GetAxis("Vertical");
            if ((Mathf.Abs(vert1) > Mathf.Abs(vert2)) && (Mathf.Abs(vert1) > Mathf.Abs(vert3)))
                vert = vert1;
            else if ((Mathf.Abs(vert2) > Mathf.Abs(vert1)) && (Mathf.Abs(vert2) > Mathf.Abs(vert3)))
                vert = vert2;
            else
                vert = vert3;

            float hori1 = Input.GetAxis(InputTagManager.GetThumbHori(HandSide.Left));
            float hori2 = Input.GetAxis(InputTagManager.GetThumbHori(HandSide.Right));
            float hori3 = Input.GetAxis("Horizontal");
            if ((Mathf.Abs(hori1) > Mathf.Abs(hori2)) && (Mathf.Abs(hori1) > Mathf.Abs(hori3)))
                hori = hori1;
            else if ((Mathf.Abs(hori2) > Mathf.Abs(hori1)) && (Mathf.Abs(hori2) > Mathf.Abs(hori3)))
                hori = hori2;
            else
                hori = hori3;

            bool flag = false;
            if (Mathf.Abs(vert) > 0.25f)
            {
                Vector3 tmp = new Vector3(_camera.transform.forward.x, 0, _camera.transform.forward.z);
                transform.position += Time.deltaTime * vert * WalkSpeed * tmp;
                isWalking = true;
                flag = true;
            }
            if (Mathf.Abs(hori) > 0.25f)
            {
                Vector3 tmp = new Vector3(_camera.transform.right.x, 0, _camera.transform.right.z);
                transform.position += Time.deltaTime * hori * WalkSpeed * tmp;
                isWalking = true;
                flag = true;
            }
            else if(!flag)
            {
                isWalking = false;
            }
        }
        else 
            isWalking = false;

#if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Space))
        {
            var rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 4.5f, rb.velocity.z);
        }
#endif

    }
}
