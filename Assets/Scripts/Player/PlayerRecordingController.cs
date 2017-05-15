using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecordingController : MonoBehaviour
{
    public int RecordingIndex;
    public List<ControlsFrame> Recording;
    public Player PlayerScript;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlsFrame frame = new ControlsFrame(Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"),
            Input.GetButtonDown("Fire1"),
            Input.GetButtonDown("Jump"),
            Input.GetButton("Crouch"),
            Input.GetButton("Run"),
            Input.GetKeyDown(KeyCode.P));
        Recording.Add(frame);

        if (Input.GetKeyUp(KeyCode.R))
        {
            Destroy(gameObject);
            PlayerScript.WakeUp();
        }
    }
}
