using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public Transform Cam;
    public GameObject Bullet;
    public int Health = 10;
    public List<ControlsFrame>[] Recordings;
    public bool FakeLocalPlayer = false;//I don't love this but we don't have many days left
    public FlagController Flag;

    Transform trans;
    InputProviderBase inputProvider;
    Collider collider;
    Rigidbody rigid;
    RigidbodyFirstPersonController firstPersonController;
    bool recordMode;

    void Start()
    {
        trans = GetComponent<Transform>();
        inputProvider = GetComponent<InputProviderBase>();
        collider = GetComponent<Collider>();
        rigid = GetComponent<Rigidbody>();
        if (!isLocalPlayer && !FakeLocalPlayer)
        {
            Destroy(GetComponent<RigidbodyFirstPersonController>());
            Destroy(Cam.GetComponent<AudioListener>());
            Destroy(Cam.GetComponent<FlareLayer>());
            Destroy(Cam.GetComponent<Camera>());
        }
        else
        {
            if (isLocalPlayer)
            {

                gameObject.tag = "Player1";
            }
            firstPersonController = GetComponent<RigidbodyFirstPersonController>();
            Recordings = new List<ControlsFrame>[3];
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isServer)
        {
            if (collision.transform.tag == "Bullet")
            {
                Health--;
            }
        }
    }
    void Update()
    {
        if (isLocalPlayer || FakeLocalPlayer)
        {
            if (inputProvider.Controls.Shoot)
            {
                shoot();
            }
            if (inputProvider.Controls.DropFlag)
            {
                if (Flag != null)
                {
                    Flag.DropFlag();
                }
            }
        }
        if (isLocalPlayer)
        {   
            #region Recordings
            if (Input.GetKeyUp(KeyCode.R))
            {
                recordMode = !recordMode;
            }
            if (recordMode)
            {
                int recordingIndex = -1;
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    recordingIndex = 0;
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    recordingIndex = 1;
                }
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    recordingIndex = 2;
                }
                if (recordingIndex > -1)
                {
                    GameObject recorder = Instantiate(NetworkManager.singleton.playerPrefab);
                    recorder.tag = "Recorder";
                    Destroy(recorder.GetComponent<NetworkTransformChild>());
                    Destroy(recorder.GetComponent<NetworkTransform>());
                    recorder.GetComponent<Player>().FakeLocalPlayer = true;
                    PlayerRecordingController rec = recorder.AddComponent<PlayerRecordingController>();
                    rec.PlayerScript = this;
                    rec.RecordingIndex = recordingIndex;
                    if (Recordings[recordingIndex] == null)
                    {
                        Recordings[recordingIndex] = new List<ControlsFrame>();
                    }
                    else
                    {
                        Recordings[recordingIndex].Clear();
                    }
                    rec.Recording = Recordings[recordingIndex];
                    Transform recTransform = recorder.GetComponent<Transform>();
                    recTransform.position = trans.position;
                    recTransform.rotation = trans.rotation;
                    Sleep();
                }
            }
            else
            {
                int recordingIndex = -1;
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    recordingIndex = 0;
                }
                if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    recordingIndex = 1;
                }
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    recordingIndex = 2;
                }
                if (recordingIndex > -1)
                {
                    if (Recordings[recordingIndex] != null)
                    {
                        CmdSpawnShadow(recordingIndex);
                    }
                }
            }
            #endregion
        }
    }
    //Disables the player so that you can do the recording
    public void Sleep()
    {
        this.enabled = false;
        firstPersonController.enabled = false;
        collider.enabled = false;
        rigid.isKinematic = true;
        Cam.GetComponent<Camera>().enabled = false;
        Cam.GetComponent<FlareLayer>().enabled = false;
        Cam.GetComponent<AudioListener>().enabled = false;
    }
    public void WakeUp()
    {
        this.enabled = true;
        firstPersonController.enabled = true;
        collider.enabled = true;
        rigid.isKinematic = false;
        Cam.GetComponent<Camera>().enabled = true;
        Cam.GetComponent<FlareLayer>().enabled = true;
        Cam.GetComponent<AudioListener>().enabled = true;
        recordMode = false;
    }

    void shoot()
    {
        if (FakeLocalPlayer)
        {
            GameObject bull = Instantiate<GameObject>(Bullet);
            Rigidbody bullRigid = bull.GetComponent<Rigidbody>();
            bullRigid.position = Cam.position + Cam.forward;
            bullRigid.velocity = Cam.forward * 20;
            return;
        }
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
        bull.GetComponent<Transform>().position = Cam.position + Cam.forward;
        bullRigid.position = Cam.position + Cam.forward;
        bullRigid.velocity = Cam.forward * 20;
    }
    [Command]
    void CmdSpawnShadow(int recordingIndex)
    {
        GameObject shadow = Instantiate<GameObject>(NetworkManager.singleton.spawnPrefabs[0]);
        Transform shadowTransform = shadow.GetComponent<Transform>();
        shadowTransform.position = trans.position;
        shadowTransform.eulerAngles = trans.eulerAngles;
        NetworkServer.SpawnWithClientAuthority(shadow, connectionToClient);
        shadow.GetComponent<Player>().RpcInitShadow(recordingIndex);
    }
    [ClientRpc]
    void RpcInitShadow(int recordingIndex)
    {
        if (hasAuthority)
        {
            GameObject playerGO = GameObject.FindGameObjectWithTag("Player1");
            Player player = playerGO.GetComponent<Player>();
            GetComponent<RecordedInputProvider>().Recording = player.Recordings[recordingIndex];
            Transform playerTrans = playerGO.GetComponent<Transform>();
            trans.position = playerTrans.position;
            trans.eulerAngles = playerTrans.eulerAngles;
        }
    }
    [Command]
    void CmdDropFlag()
    {
        Flag.DropFlag();
    }
}
