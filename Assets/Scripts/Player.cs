using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Transform Cam;
    public GameObject Bullet;
    public int Health=10;

    void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>());
            Destroy(Cam.GetComponent<AudioListener>());
            Destroy(Cam.GetComponent<FlareLayer>());
            Destroy(Cam.GetComponent<Camera>());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.other.tag=="Bullet")
        {
            Health--;
        }
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                shoot();
            }
        }
    }

    void shoot()
    {
        if (isServer)
        {
            RpcShoot();
        }
        else
        {
            CmdShoot();
        }
    }
    [Command]
    void CmdShoot()
    {
        RpcShoot();
    }
    [ClientRpc]
    void RpcShoot()
    {
        GameObject bull = Instantiate<GameObject>(Bullet);
        Rigidbody bullRigid = bull.GetComponent<Rigidbody>();
        bullRigid.position = Cam.position + Cam.forward;
        bullRigid.velocity = Cam.forward * 20;
    }

}
