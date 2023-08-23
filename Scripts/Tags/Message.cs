using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message 
{
    public string[] message;

    int i = 0;

    public Message()
    {
        message = new string[Language.GetLanguageCount()];
    }


    public void AddMessage(string _message)
    {
        message[i] = _message;
        i++;
    }

    public string GetMessage()
    {
        return message[Language.GetPreference()];
    }
    

}
