using System;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    Server server;
    //findPlayersHosting fph;

    public GameObject attackCanvasprefab;
    GameObject attackCanvas;

    MessageSystem messageSystem;
    ChangePlaymatColor changePlaymatColor;

    [SyncVar]public string Playername = "";
    [SyncVar]public int id;
    [SyncVar]public bool isPlayerTurn = false;

    [Header("Set negative value for default.")]
    [SyncVar]public int Health = -1;
    [SyncVar]public int Speed = -1;

    [SyncVar]public int Damage = -1;
    [SyncVar]public int Defence = -1;
    [SyncVar]public int Range = -1;


    public override void OnStartClient()
    {
        server = GameObject.FindObjectOfType<Server>();
        server.AddPlayerToList(this);
        
        messageSystem = GameObject.FindObjectOfType<MessageSystem>();
        changePlaymatColor = GameObject.FindObjectOfType<ChangePlaymatColor>();
    }
    private void Update() {

        if (!server)
        {
            server = GameObject.FindObjectOfType<Server>();
            if(server) server.AddPlayerToList(this);
        }
        if (isLocalPlayer) if (Input.GetKeyDown(KeyCode.L)) NewMessage(Playername + ": Debug message lol!");


        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                NewColor();
            }
        }

        if (Input.GetKeyDown(KeyCode.C)) NewColor();
    }

    public void ShowIntroductoryMessage()
    {
        messageSystem.RpcNewMessage(Playername + " has joined!");
    }

    [Command]
    void CmdSetHealth(int value)
    {
        Health = value;
    }

    public void InitiateTurn()
    {
        isPlayerTurn = true;
        messageSystem.RpcNewMessage(Playername +"'s turn.");
    }


    [ClientRpc]
    public void RpcSendClientMessage(string message)
    {
        if(isLocalPlayer)FindObjectOfType<ObjectReferences>().messageManager.AddNewMessageToBuffer(message);
    }

    void NewMessage(string message)
    {
        if (isServer)
        {
            CmdSendMsgToMsgSys(message);
            return;
        }
        foreach (Player player in FindObjectsOfType<Player>())
        {
            player.CmdSendMsgToMsgSys(message);
        }
    }
    [Command]
    public void CmdSendMsgToMsgSys(string message)
    {
        if(isServer)
        messageSystem.RpcNewMessage(message);
    }

    [ClientRpc]
    public void RpcChangeCameraView()
    {
        if (isLocalPlayer) FindObjectOfType<ObjectReferences>().closeLobby();
    }

    void NewColor()
    {
        if (isServer)
        {
            CmdSendcolorToSys();
            return;
        }
        foreach (Player player in FindObjectsOfType<Player>())
        {
            player.CmdSendcolorToSys();
        }
    }

    [Command]
    public void CmdSendcolorToSys()
    {
        Debug.Log("sendcolor");
        if (isServer)
        changePlaymatColor.NewPlaymatColor();
    }

    [ClientRpc]
    public void RpcChangePlaymatColor(Color newcolor)
    {
        Debug.Log("recivecolor");
        GameObject gos = GameObject.FindGameObjectWithTag("PlayMat");

        if (isLocalPlayer) gos.GetComponent<Renderer>().material.color = newcolor;
    }



    public void SetDefaultValues()
    {
        if(Health < 0)
        {
            Health = PlayerDefaultValues.Player_BaseHealth;
        }
        if (Speed < 0)
        {
            Speed = PlayerDefaultValues.Player_BaseSpeed;
        }
        if (Damage < 0)
        {
            Damage = PlayerDefaultValues.Player_BaseDamage;
        }
        if (Defence < 0)
        {
            Defence = PlayerDefaultValues.Player_BaseDefence;
        }
        if (Range < 0)
        {
            Range = PlayerDefaultValues.Player_BaseRange;
        }
    }
}


