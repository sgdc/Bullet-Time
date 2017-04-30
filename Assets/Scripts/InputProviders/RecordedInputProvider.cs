using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordedInputProvider : InputProviderBase
{
    public List<ControlsFrame> Recording;
    private ControlsFrame _controls;
    public override ControlsFrame Controls
    {
        get
        {
            return _controls;
        }
    }

    int frameIndex = 0;
    int lastFrameIndex = 0;
    float playTime = 0;


    void Start()
    {
        _controls = new ControlsFrame();
    }

    void Update()
    {
        if (Recording != null)
        {
            if (frameIndex < Recording.Count)
            {
                lastFrameIndex = frameIndex;
                List<ControlsFrame> passedFrames = new List<ControlsFrame>();
                playTime += Time.deltaTime;
                while (frameIndex != Recording.Count && playTime > Recording[frameIndex].TimeStamp)
                {
                    passedFrames.Add(Recording[frameIndex]);
                    frameIndex++;
                }
                if (lastFrameIndex == frameIndex)
                {
                    _controls.Shoot = false;
                }
                else
                {
                    _controls = ControlsFrame.CollapseFrames(passedFrames);
                }
            }
            else
            {
                Recording = null;
                _controls = new ControlsFrame();
            }
        }
    }
}
