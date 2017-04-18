using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveInputProvider : InputProviderBase
{
    private ControlsFrame _controls = new ControlsFrame();
    public override ControlsFrame Controls
    {
        get
        {
            return _controls;
        }
    }

    void Update()
    {
        _controls = new ControlsFrame(Input.GetAxis("Horizontal"), 
            Input.GetAxis("Vertical"), 
            Input.GetButtonDown("Fire1"), 
            Input.GetButtonDown("Jump"), 
            Input.GetButton("Crouch"), 
            Input.GetButton("Run"));
    }
}
