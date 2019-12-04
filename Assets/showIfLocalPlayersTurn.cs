using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showIfLocalPlayersTurn : MonoBehaviour
{
    Player player;
    CanvasGroup canvas;

    private void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        CanvasControls.SetCanvas(canvas, false);
    }

    void TryFindPlayer()
    {
        foreach (var _player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (_player.TryGetComponent<Player>(out Player playerScript))
            {
                if (playerScript.isLocalPlayer)
                {
                    player = playerScript;
                    break;
                }
            }
        }
    }


    void Update()
    {
        if (!player) TryFindPlayer();
        else
        {
            if (player.isPlayerTurn)
            {
                CanvasControls.SetCanvas(canvas, true);
            }
            else
            {
                CanvasControls.SetCanvas(canvas, false);
            }
        }
    }
}
