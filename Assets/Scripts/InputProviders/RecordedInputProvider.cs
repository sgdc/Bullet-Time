using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedInputProvider : InputProviderBase
{
    public List<ControlsFrame> Recording;
    public override ControlsFrame Controls
    {
        get
        {
            return Recording[frameIndex];
        }
    }

    int frameIndex = 0;


    void Start()
    {

    }
    
    void Update()
    {
        frameIndex++;
        if (frameIndex>=Recording.Count)
        {
            frameIndex = Recording.Count - 1;
        }
    }
}
