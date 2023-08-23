using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{

    // Start is called before the first frame update
    public GameObject fire;
    AudioSource fireSound;
    [SerializeField] BulletPool bulletPool;
    public GameObject spawnPosition_pistol;
    public GameObject spawnPosition_mg;
    public GameObject shotgunSpawnPositions;
    private List<Transform> points = new List<Transform>();
    // public GameObject offset;
    [SerializeField] Constants.FIRE_OPTION fire_option;

    delegate void FireCallback();
    FireCallback fireCallback;
    private bool fireEnabled = true;

    //Shotgun Specs
    Vector3[] posArr = new Vector3[9];
    Quaternion[] range = new Quaternion[9];
    float posVarCorner = 0.05f;
    float posVarEdge = 0.06f;
    float angle = 2f;


    private void Awake()
    {
        foreach (Transform spawnPositionSG in shotgunSpawnPositions.GetComponentsInChildren<Transform>()
        )
        {
            points.Add(spawnPositionSG);
        }

        switch (fire_option)
        {
            case Constants.FIRE_OPTION.GUN:
                fireCallback = new FireCallback(Fire_Gun);
                break;
            case Constants.FIRE_OPTION.SHOTGUN:
                fireCallback = new FireCallback(Fire_Shotgun);
                break;
            case Constants.FIRE_OPTION.MACHINE_GUN:
                fireCallback = new FireCallback(Fire_Machine);
                break;
        }



        //range[0] = Quaternion.Euler(-angle - 1, 0, 0);
        //range[1] = Quaternion.Euler(-angle, angle, 0);
        //range[2] = Quaternion.Euler(0, angle + 1, 0);
        //range[3] = Quaternion.Euler(angle, angle, 0);
        //range[4] = Quaternion.Euler(angle + 1, 0, 0);
        //range[5] = Quaternion.Euler(angle, -angle, 0);
        //range[6] = Quaternion.Euler(0, -angle - 1, 0);
        //range[7] = Quaternion.Euler(-angle, -angle, 0);
        //range[8] = Quaternion.Euler(0, 0, 0);

    }

    private void Start()
    {
        fire = GameObject.Find("fire");
        fireSound = fire.GetComponent<AudioSource>();
    }
    private void Update()
    {
        fireCallback();
    }
    // Firing bullet with mouse input

    public void ChangeFireCallback(Constants.FIRE_OPTION gunType){
        fire_option = gunType;
        switch (fire_option)
        {
            case Constants.FIRE_OPTION.GUN:
                fireCallback = new FireCallback(Fire_Gun);
                break;
            case Constants.FIRE_OPTION.SHOTGUN:
                fireCallback = new FireCallback(Fire_Shotgun);
                break;
            case Constants.FIRE_OPTION.MACHINE_GUN:
                fireCallback = new FireCallback(Fire_Machine);
                break;
        }
    }
    void Fire_Machine()
    {

        float inputValue = Input.GetAxis(InputTagManager.GetIndex(HandSide.Right));
        bool isClicking = inputValue >= InputTagManager.VRInputThreshold || Input.GetMouseButtonDown(0);

        if (isClicking)
        {
            fireSound.Play();
            Bullet bullet = bulletPool.GetBullet(spawnPosition_mg.transform.position, transform.rotation);

        }

    }

    void Fire_Gun()
    {
        float inputValue = Input.GetAxis(InputTagManager.GetIndex(HandSide.Right));
        bool isClicking = inputValue >= InputTagManager.VRInputThreshold || Input.GetMouseButtonDown(0);
        if (isClicking)
        {
            if (fireEnabled)
            {
                fireEnabled = false;
                fireSound.Play();
                Bullet bullet = bulletPool.GetBullet(spawnPosition_pistol.transform.position, spawnPosition_pistol.transform.rotation   );
            }

        }
        else
        {
            fireEnabled = true;
        }

    }

    void Fire_Shotgun()
    {
       
        float inputValue = Input.GetAxis(InputTagManager.GetIndex(HandSide.Right));
        bool isClicking = inputValue >= InputTagManager.VRInputThreshold || Input.GetMouseButtonDown(0);
        if (isClicking)
        {
            if (fireEnabled)
            {
                fireEnabled = false;
                fireSound.Play();
                ShotgunSpawn();
            }

        }
        else
        {
            fireEnabled = true;
        }

    }

    void ShotgunSpawn()
    {
        //posArr[0] = spawnPosition.transform.position + transform.up * posVarEdge;
        //posArr[1] = spawnPosition.transform.position + transform.up * posVarCorner + transform.right * posVarCorner;
        //posArr[2] = spawnPosition.transform.position + transform.right * posVarEdge;
        //posArr[3] = spawnPosition.transform.position + transform.up * -posVarCorner + transform.right * posVarCorner;
        //posArr[4] = spawnPosition.transform.position + transform.up * -posVarEdge;
        //posArr[5] = spawnPosition.transform.position + transform.up * -posVarCorner + transform.right * -posVarCorner;
        //posArr[6] = spawnPosition.transform.position + transform.right * -posVarEdge;
        //posArr[7] = spawnPosition.transform.position + transform.up * posVarCorner + transform.right * -posVarCorner;
        //posArr[8] = spawnPosition.transform.position;

        //for (int i = 0; i < 9; i++)
        //{
        //Bullet bullet = bulletPool.GetBullet(posArr[i],
        //transform.rotation);
        //bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(bullet.transform.forward) * 0.01f;

        //}

        for (int i = 1; i < points.Count; i++)
        {
            Transform spawnTransform = points[i];

            Bullet bullet = bulletPool.GetBullet(spawnTransform.position,
        spawnTransform.rotation);
            
        }
    }
}
