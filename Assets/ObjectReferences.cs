using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ObjectReferences : MonoBehaviour
{
    public messageManager messageManager;
    public Player localPlayer;
    public GameObject ARCam;
    public GameObject mainCam;
    public GameObject LobbyCanvas;

    // Update is called once per frame
    void Update()
    {
        if (!messageManager) messageManager = FindObjectOfType<messageManager>();
        if (!localPlayer) {
           Player[] activePlayers = FindObjectsOfType<Player>();
            foreach (var player in activePlayers)
            {
                if (player.isLocalPlayer) localPlayer = player;
                break;
            }
        }
        if (!ARCam) ARCam = GameObject.Find("ARCamera");
        if (!mainCam) mainCam = GameObject.Find("Main Camera");
        if (!LobbyCanvas) LobbyCanvas = GameObject.Find("Lobby");
    }
    //<param> set to true to enable AR camera and disable Lobby
    public void closeLobby(bool ar = true)
    {
        ARCam.SetActive(ar);
        mainCam.SetActive(!ar);
        LobbyCanvas.SetActive(!ar);
    }
}
