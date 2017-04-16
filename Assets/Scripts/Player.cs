using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public GameObject Cam;

    void Start()
    {
        if (isLocalPlayer)
        {
            Cam.SetActive(true);
        }
    }
    
    void Update()
    {

    }
}
