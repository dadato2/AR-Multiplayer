using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HostOrJoinScript : MonoBehaviour
{
    [SerializeField] NetworkManager network;

    public void StartMatch()
    {
        network.StartMatchMaker();
    }
    public void StopMatch()
    {
        network.StopMatchMaker();
    }

    public void Host(findPlayersHosting host)
    {
        string name = "default";
        if (host.PlayerName != null && host.PlayerName.Length >= 3) name = host.PlayerName;
        network.matchName = name;
        network.matchSize = 4;
        network.matchMaker.CreateMatch(network.matchName, network.matchSize, true, "", "", "", 0, 0, network.OnMatchCreate);

        Debug.Log("Hosting game as \"" + network.matchName + "\"");

    }
    
    public void StopHost()
    {
        network.StopHost();
        StartMatch();
        Debug.Log("Stopping host");
    }

    public void SearchHosts()
    {
        network.matchMaker.ListMatches(0, 20, "", false, 0, 0, network.OnMatchList);
    }

    public void JoinGame(string name, string PlayerName)
    {
        for(int i = 0; i < network.matches.Count; i ++)
        {
            var match = network.matches[i];
            if (match.name == name)
            {
                network.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, network.OnMatchJoined);
                //NetworkOverride.MyAddPlayer(PlayerName, 0);
                //Server.setNewPlayerName(PlayerName);
            }
        }
    }

    public void StopJoin()
    {
        network.StopMatchMaker();
        StartMatch();
    }
}
