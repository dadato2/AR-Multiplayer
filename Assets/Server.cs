using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server : NetworkBehaviour
{
    public List<Player> playerList;
    bool startedplayer = false;

    public float timer = 10f;

    public void AddPlayerToList(Player player)
    {
        if (playerList.Count > 0)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if (playerList[i].id != i) playerList[i].id = i;
            }
        }
        playerList.Add(player);
        player.id = playerList.Count - 1;
        player.SetDefaultValues();
        //player.ShowIntroductoryMessage();
    }
    private void Start()
    {

    }

    private void Update()
    {
        if(timer>0)timer -= Time.deltaTime;
        else
        {
            if (startedplayer) NextTurn();
            else StartTurn();
            timer = 10f;
        }
    }


    public void StartTurn()
    {
        playerList[0].isPlayerTurn = true;
        if (playerList.Count > 1)
        {
            for (int i = 1; i < playerList.Count; i++)
            {
                playerList[i].isPlayerTurn = false;
            }
        }
    }

    public void NextTurn()
    {
        if (playerList.Count > 1)
        {
            print("more players are here, next turn");
            if (playerList[playerList.Count - 1].isPlayerTurn == true)
            {
                print("last player is not ");
                playerList[playerList.Count - 1].isPlayerTurn = false;
                playerList[0].isPlayerTurn = true;
                playerList[0].InitiateTurn();
            }
            else
            {
                for (int i = 1; i < playerList.Count-1; i++)
                {
                    if (playerList[i].isPlayerTurn == true)
                    {
                        print("the next player");
                        playerList[i].isPlayerTurn = false;
                        playerList[i+1].isPlayerTurn = true;
                        playerList[i+1].InitiateTurn();
                        break;
                    }
                }
            }

        }
    }


}
