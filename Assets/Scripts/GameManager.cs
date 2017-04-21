using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    public GameObject WinDisplay;
    public GameObject LoseDisplay;

    // Use this for initialization
    void Start()
    {
        _instance = this;
    }

    public void FlagCapture(bool serverCapture)
    {
        RpcGameFinished(serverCapture);
        StartCoroutine(closeServer());
    }
    IEnumerator closeServer()
    {
        yield return new WaitForSeconds(3);
        NetworkManager.singleton.StopHost();
    }

    [ClientRpc]
    void RpcGameFinished(bool hostWins)
    {
        if (hostWins==isServer)
        {
            //Win
        }
        else
        {
            //Lose
        }
    }
}
