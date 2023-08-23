using UnityEngine;

public enum PlayerType
{
    VR,
    PC
}

public class Player : MonoBehaviour
{
    public static Player Main { get; private set; }
    public static Camera Camera { get; private set; }
    public static PlayerType PlayerType { get; private set; }

    [SerializeField] GameObject raycast_hand_object_left;
    [SerializeField] GameObject raycast_hand_object_right;

    [SerializeField] GameObject machinegun;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;

    static PlayerRaycast raycast_left = null;
    static PlayerRaycast raycast_right = null;

    static LineRenderer line_renderer_left = null;
    static LineRenderer line_renderer_right = null;

    public GameObject current_weapon;

    private void OnEnable()
    {
        Main = this;
        Camera = GetComponent<Camera>();
        if(Camera == null)
        {
            Camera = GetComponentInChildren<Camera>();
        }
        if (gameObject.name.Contains("VR"))
            PlayerType = PlayerType.VR;
        else if (gameObject.name.Contains("PC"))
            PlayerType = PlayerType.PC;
        else
            Debug.LogError("Player type cannot be determined!");

        if(raycast_hand_object_left)
            raycast_left = raycast_hand_object_left.GetComponent<PlayerRaycast>();
        if (raycast_hand_object_right)
            raycast_right = raycast_hand_object_right.GetComponent<PlayerRaycast>();
    }

    public static void EnableDisableRaycast(bool enable = true)
    {
        if (raycast_left)
        {
            raycast_left.enabled = enable;
            line_renderer_left.enabled = enable;
        }

        if (raycast_right)
        {
            raycast_right.enabled = enable;
            line_renderer_right.enabled = enable;
        }            
    }


    public static bool IsVRPlayer => PlayerType == PlayerType.VR;

    public static bool IsPCPlayer => PlayerType == PlayerType.PC;

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Machine")
        {
            other.gameObject.SetActive(false);
            current_weapon.gameObject.SetActive(false);
            current_weapon = machinegun;
            current_weapon.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "Shotgun")
        {
            other.gameObject.SetActive(false);
            current_weapon.gameObject.SetActive(false);
            current_weapon = shotgun;
            current_weapon.gameObject.SetActive(true);
        }
        else if (other.gameObject.tag == "Pistol")
        {
            other.gameObject.SetActive(false);
            current_weapon.gameObject.SetActive(false);
            current_weapon = pistol;
            current_weapon.gameObject.SetActive(true);
        }
    }


}
