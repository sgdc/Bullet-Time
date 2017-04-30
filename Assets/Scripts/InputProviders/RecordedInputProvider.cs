using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RecordedInputProvider : InputProviderBase
{
    public List<ControlsFrame> Recording;
    public override ControlsFrame Controls
    {
        get
        {
            if (Recording == null)
            {
                return new ControlsFrame();
            }
            return Recording[frameIndex];
        }
    }

    int frameIndex = 0;
    bool playing=true;

    void Start()
    {

    }

    void Update()
    {
        if (Recording != null)
        {
            frameIndex++;
            if (frameIndex >= Recording.Count)
            {
                frameIndex = Recording.Count - 1;
                if (playing)
                {
                    playing = false;
                    FinishedPlayBack();
                }
            }
        }
    }
    void FinishedPlayBack()
    {
        GetComponent<Player>().PlaybackFinished();
    }
}
