using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KKDRoomManager : MonoBehaviour
{
    public static KKDRoomManager Instance { get; private set; }
    [SerializeField] public int trueKKD;
    public enum STATE { START = 0, OTHERSCENE =1 }
    public STATE game_state = STATE.START;


    [SerializeField] private Canvas own_canvas;
    [SerializeField] InfoMessages.MESSAGE_ID selectMessage;
    private TextMeshProUGUI canvasText;
    [SerializeField] private Animator menuAnimator;
    [SerializeField] GameObject hologram;

    private void Start()
    {
        if (own_canvas != null)
        {
            canvasText = own_canvas.GetComponentInChildren<TextMeshProUGUI>();
        }

        menuAnimator.SetBool("menuin", true);
    }
    private void Awake()
    {
        Instance = this;
    }
    public void State()
    {
        switch (game_state)
        {
            case STATE.START:
                CheckTrueKKD();
                break;
            case STATE.OTHERSCENE:
                break;
        }
    }
    public void CheckTrueKKD()
    {
        if(trueKKD == 4)
        {
            game_state = STATE.OTHERSCENE;
            Invoke("ShowMainInfo", 1.5f);
            Invoke("ChangeScene", 0.1f);
        }
    }

    private void WriteOwnCanvas(InfoMessages.MESSAGE_ID message)
    {
        if (own_canvas)
            canvasText.text = InfoMessages.GetMessage(message);
    }

    private void EraseOwnCanvas()
    {
        if (own_canvas)
            canvasText.text = "";
    }

    private void SetOwnCanvasActive(bool isActive)
    {
        if (own_canvas != null)
            own_canvas.enabled = isActive;
    }
    void ChangeScene()
    {
        hologram.SetActive(true);
    }
    private void ShowMainInfo()
    {
        SetOwnCanvasActive(true);
        menuAnimator.SetBool("menuin", false);
        WriteOwnCanvas(selectMessage);
    }
}
