using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CamController : MonoBehaviour
{
    CharacherController characterController;
    [SerializeField] BulletPool bulletPool;


   
    public GameObject bulletPrefab;
    private float startYscale = 1.8f;
    [SerializeField] Camera Cam;



    [SerializeField]
    private float crouchHeight;


    float mousex;


    Rigidbody rb;

    //clampRotation: max angle that mouse can reach in vertically
    float clampRotation = Constants.CLAMP_ROTATION;

    private void Start()
    {
        

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacherController>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Crouch();
    }

    private void FixedUpdate()
    {
        // player movement depends on the inputs
        
    }

    // Camera position with mouse
    

    // Crouching with keyboard input
    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale =
                new Vector3(transform.localScale.x,
                    crouchHeight,
                    transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale =
                new Vector3(transform.localScale.x,
                    startYscale,
                    transform.localScale.z);
        }
    }

    
    
}
