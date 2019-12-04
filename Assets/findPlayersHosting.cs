using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class findPlayersHosting : NetworkBehaviour
{
    public string PlayerName;
    public bool playerref;

    float timeToReftesh = 1f;
    float refreshTimer = 0f;
    bool hasJoinedGame = false;

    [SerializeField] NetworkManager network;
    [SerializeField] HostOrJoinScript hostOrJoin;


    bool hasFoundGame = false;

    [SerializeField]GameObject button;
    Button button_button;
    [SerializeField] GameObject button2;
    Button button_button2;


    void Start()
    {
        localReferences.localPlayer = this;
        button_button = button.GetComponent<Button>();
        button.SetActive(false);
        button_button2 = button2.GetComponent<Button>();
        button2.SetActive(false);
    }

    public void SetPlayerName(InputField name)
    {
        PlayerName = name.text;
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerref)
        {
            foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if(player.GetComponent<Player>().isLocalPlayer)
                {
                    playerref = player.GetComponent<Player>();
                    //playerref.CmdSetMyName(PlayerName);
                    NetworkOverride.MyAddPlayer(PlayerName, player.GetComponent<NetworkIdentity>().netId);
                    //Destroy(player);
                    playerref = true;
                    break;
                }
            }
        }
        if (network.matches != null && !hasJoinedGame)
        {
            if(refreshTimer< timeToReftesh)
            {
                refreshTimer += Time.deltaTime;
            }
            else
            {
                network.matchMaker.ListMatches(0, 20, "", false, 0, 0, network.OnMatchList);
                refreshTimer = 0;
            }
        }
        if(!hasJoinedGame){
            if (hasFoundGame) {
                if (network.matches != null)
                {
                    if (network.matches.Count >= 1)
                    {
                        button.SetActive(true);
                        button_button.GetComponentInChildren<Text>().text = "Join " + network.matches[0].name;
                        button_button.onClick.RemoveAllListeners();
                        button_button.onClick.AddListener(delegate ()
                        {
                            hostOrJoin.JoinGame(network.matches[0].name, PlayerName);
                        });

                        hasJoinedGame = true;
                    }
                    if(network.matches.Count >= 2)
                    {
                        button2.SetActive(true);
                        button_button2.GetComponentInChildren<Text>().text = "Join " + network.matches[1].name;
                        button_button2.onClick.RemoveAllListeners();
                        button_button2.onClick.AddListener(delegate ()
                        {
                            hostOrJoin.JoinGame(network.matches[1].name, PlayerName);
                        });

                        hasJoinedGame = true;
                    }
                }
                else hasFoundGame = false;
            }
            else
            {
                if (network.matches != null)
                {
                    if (network.matches.Count < 1)
                    {
                        button.SetActive(false);
                    }
                    else {
                        hasFoundGame = true;
                    }
                }
            }
        }
    }

}
