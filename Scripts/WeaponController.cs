using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{



    public GameObject shotgun;
    public GameObject pistol;
    public GameObject machineGun;
    private Constants.FIRE_OPTION activeGun = Constants.FIRE_OPTION.GUN;
    [SerializeField] Constants.FIRE_OPTION fire_option;

    public bool IsGrabbing ;
    public bool canGrab;
    // Start is called before the first frame update
    void Start()
    {
        SetWeapon(fire_option);
    }

    
    // Update is called once per frame
    private void OnTriggerEnter(Collider collider){
        if (collider.gameObject.tag == "WeaponPlayer")
            canGrab = true;
    }

    // private void OnTriggerExit(Collider collider){
    //     canGrab = false;
    // }
    private void OnTriggerStay(Collider collision){
        IsGrabbing = Input.GetAxis(InputTagManager.GetGrip(HandSide.Right)) >= InputTagManager.VRInputThreshold
                                      || Input.GetMouseButton(1);
        if (collision.gameObject.tag == "WeaponPlayer" && IsGrabbing && canGrab)
        {
            canGrab = false;
            Debug.Log("collision with " + collision.gameObject);

            WeaponChanger(collision.gameObject.GetComponent<WeaponController>());

        }
    }

    void WeaponChanger(WeaponController weapon)
    {
        Constants.FIRE_OPTION otherGun = weapon.GetActiveGun();   
        Debug.Log("collided weapon: " + otherGun);
        weapon.SetWeapon(this.activeGun);
        Fire script = weapon.GetComponent<Fire>();
        script.ChangeFireCallback(this.activeGun);
        //weapon.SetWeapon(activeGun);
        this.SetWeapon(otherGun);
        
    }


    public Constants.FIRE_OPTION GetActiveGun()
    {
        return activeGun;
    }

    public void SetWeapon(Constants.FIRE_OPTION fire)
    {

        bool isActivePistol = false;
        bool isActiveMachine = false; 
        bool isActiveShotgun = false;
        this.activeGun = fire;
        switch(activeGun)
        {
            case Constants.FIRE_OPTION.GUN:
                isActivePistol = true;

                break;
            case Constants.FIRE_OPTION.SHOTGUN:
                isActiveShotgun = true;
                
                break;
            case Constants.FIRE_OPTION.MACHINE_GUN:
                isActiveMachine = true;
                
                break;


        }

        ChangeWeapons(pistolBool: isActivePistol, machineBool: isActiveMachine, shotgunBool: isActiveShotgun);

    }

    void ChangeWeapons(bool pistolBool, bool machineBool, bool shotgunBool)
    {
        Debug.Log(pistolBool.ToString() + machineBool.ToString() + shotgunBool.ToString());
        shotgun.SetActive(shotgunBool);
        machineGun.SetActive(machineBool);
        pistol.SetActive(pistolBool);
    }

}
