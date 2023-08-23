using UnityEngine;
using UnityEngine.SceneManagement;

public class VRUtilities : MonoBehaviour
{
    private readonly string[] _inputTags = new string[]
    {
        "XRI_Left_PrimaryButton",
        "XRI_Left_SecondaryButton",
         "XRI_Right_PrimaryButton",
        "XRI_Right_SecondaryButton"
    };

    private string[] InputTags => _inputTags;
    private bool[] _isPressed;


    private PlayerChanger _playerChanger;

    private void Awake()
    {
        _isPressed = new bool[4] {false, false, false, false};

        _playerChanger = GetComponent<PlayerChanger>();
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            if(!_isPressed[i] && Input.GetAxis(InputTags[i]) >= InputTagManager.VRInputThreshold)
            {
                _isPressed[i] = true;
                Execute(i);
            }
            else if(_isPressed[i] && Input.GetAxis(InputTags[i]) < InputTagManager.VRInputThreshold)
            {
                _isPressed[i] = false;
            }
        }
    }

    private void Execute(int i)
    {
        switch(i)
        {
            case 0:
                RestartGame();
                break;
            case 1:
                ChangePlayerType();
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                Debug.Assert(false, "?");
                break;
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ChangePlayerType()
    {
        _playerChanger.ChangePlayerType();
    }


}
