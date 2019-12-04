using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class NetworkOverride : NetworkManager
{
    /*  
    public override void OnServerConnect(NetworkConnection connection)
    {
        base.OnServerConnect(connection);
        
        //MessageSystem.NewMessage("Client " + connection.connectionId + " Connected!");
    }
    public override void OnServerDisconnect(NetworkConnection connection)
    {
        base.OnServerDisconnect(connection);
        //MessageSystem.NewMessage("Client " + connection.connectionId + " disconnected!");
    }/**/

    public virtual void OnClientDisconnect(NetworkConnection conn)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        print("CLOSING MAHUDS");
    }

    public class MyAddPlayerMessage : MessageBase
    {
        public static short MSG_TYPE = 2000;
        public string name = "";
        //public short playerControllerId = 0;
        public NetworkInstanceId netID = NetworkInstanceId.Invalid;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        NetworkServer.RegisterHandler(MyAddPlayerMessage.MSG_TYPE, OnServerMyAddPlayerMessage);
        //MessageSystem.NewMessage("Started server");
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        NetworkServer.UnregisterHandler(MyAddPlayerMessage.MSG_TYPE);
        //MessageSystem.NewMessage("Stopped server");
    }


    public static void MyAddPlayer(string name, NetworkInstanceId netID)
    {
        singleton.client.Send(MyAddPlayerMessage.MSG_TYPE, new MyAddPlayerMessage() { name = name, netID = netID });
    }

    private void OnServerMyAddPlayerMessage(NetworkMessage netMsg)
    {
        MyAddPlayerMessage msg = netMsg.ReadMessage<MyAddPlayerMessage>();
        //GameObject player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        foreach(var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(player.GetComponent<NetworkIdentity>().netId == msg.netID)
            {
                player.GetComponent<Player>().Playername = msg.name;
                player.GetComponent<Player>().ShowIntroductoryMessage();
                break;
            }
        }
        //player.GetComponent<Player>().Playername = msg.name;
        //player.GetComponent<Player>().ShowIntroductoryMessage();
        //NetworkServer.AddPlayerForConnection(netMsg.conn, player, msg.playerControllerId);
        
    }/**/

}
