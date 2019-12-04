using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class messageManager : NetworkBehaviour
{
    public messageBox[] messages;
    public List<string> messagesBuffer = new List<string>();

    public void AddNewMessageToBuffer(string message)
    {
        messagesBuffer.Add(message);
    }

    public override void OnStartClient()
    {
        var pp = GameObject.Find("Server");
        var mms = pp.GetComponent<MessageSystem>();
        mms.AddSelf(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(messagesBuffer.Count>0)
        {
            ShowNewMessage();
        }
    }


    void ShowNewMessage()
    {
        string message = messagesBuffer[0];
        messagesBuffer.RemoveAt(0);
        for (int i = messages.Length-1; i > 0; i--)
        {
            messages[i].setNewMessage(messages[i-1].getMessage(), messages[i-1].getTime());
        }
        messages[0].setNewMessage(message);
    }
}
