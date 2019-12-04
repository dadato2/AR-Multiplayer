using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageSystem : NetworkBehaviour
{
    //public List<messageManager> messageManagers = new List<messageManager>();

    public void AddSelf(messageManager mm)
    {
        //messageManagers.Add(mm);
    }

    [ClientRpc]
    public void RpcNewMessage(string message)
    {
        if (message!= null && message != "")
        {
            /*foreach(var mm in messageManagers)
            {
                mm.AddNewMessageToBuffer(message);
                Debug.Log(message);
            }*/
            foreach (Player player in GetComponent<Server>().playerList)
            {
                player.RpcSendClientMessage(message);
            }
        }
    }
}
