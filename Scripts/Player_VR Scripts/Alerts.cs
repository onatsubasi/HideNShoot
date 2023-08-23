using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Alerts : MonoBehaviour
{

    [SerializeField] float interpTime;
    [SerializeField] float stillTime;
    [SerializeField] TextMeshProUGUI txt;
    Color ProcessColor;
    [SerializeField] Image NegImg, PosImg;
    Image ProcessImg;

    [SerializeField] Color correctCol, wrongCol;

    float timeElapsed;
    bool starting, still, ending;
    bool permanent;

    private void Start()
    {
        permanent = starting = still = ending = false;
    }

    public void StartAlert(string textMessage, bool positive)
    {
        if (permanent)
            return;
        if (starting || still || ending)
        {
            ProcessImg.color = ProcessColor;
            starting = still = ending = false;
        }

        if (positive)
            ProcessImg = PosImg;
        else
            ProcessImg = NegImg;

        txt.color = ProcessColor = ProcessImg.color;
        ProcessColor.a = 0;

        txt.text = textMessage;
        timeElapsed = 0f;
        starting = true;
    }

    public void StartAlertPermanent(string textMessage, bool positive)
    {
        if (permanent)
            return;
        if (starting || still || ending)
        {
            ProcessImg.color = ProcessColor;
            starting = still = ending = false;
        }

        permanent = true;
        if (positive)
            ProcessImg = PosImg;
        else
            ProcessImg = NegImg;

        txt.color = ProcessColor = ProcessImg.color;
        ProcessColor.a = 0;

        txt.text = textMessage;
        timeElapsed = 0f;
        starting = true;
    }

    public void ErasePermanentAlert()
    {
        permanent = false;
        ending = true;
    }

    private void Update()
    {

        timeElapsed += Time.deltaTime;
        if (!permanent)
        {
            if (starting)
            {
                if (timeElapsed > interpTime)
                {
                    timeElapsed = 0f;
                    still = true;
                    starting = false;
                    ProcessImg.color = txt.color = ProcessColor + Color.black;
                    return;
                }
                ProcessImg.color = txt.color = ProcessColor + Color.black * (timeElapsed / interpTime);
            }
            else if (still)
            {
                if (timeElapsed > stillTime)
                {
                    timeElapsed = 0f;
                    ending = true;
                    still = false;
                }
            }
            else if (ending)
            {
                if (timeElapsed > interpTime)
                {
                    timeElapsed = 0f;
                    ending = false;
                    ProcessImg.color = txt.color = ProcessColor;
                    return;
                }
                ProcessImg.color = txt.color = ProcessColor + Color.black * (1 - timeElapsed / interpTime);
            }
        }
        else //permanent
        {
            if (starting)
            {
                if (timeElapsed > interpTime)
                {
                    timeElapsed = 0f;
                    still = true;
                    starting = false;
                    ProcessImg.color = txt.color = ProcessColor + Color.black;
                    return;
                }
                ProcessImg.color = txt.color = ProcessColor + Color.black * (timeElapsed / interpTime);
            }
        }

    }


}
