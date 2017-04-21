using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class Lobby : MonoBehaviour
{
    public Text IPAddressText;

    // Use this for initialization
    void Start()
    {
        IPAddressText.text = Network.player.ipAddress;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
