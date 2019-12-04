using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChangePlaymatColor : NetworkBehaviour
{
    [ClientRpc]
    public void NewPlaymatColor()
    {
        foreach (Player player in GetComponent<Server>().playerList)
        {
            Color color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            player.RpcChangePlaymatColor(color);
        }
    }
}
