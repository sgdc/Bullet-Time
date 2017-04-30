using System;
using System.Collections.Generic;

public struct ControlsFrame
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public float MouseX { get; set; }
    public float MouseY { get; set; }
    public bool Shoot { get; set; }
    public bool Jump { get; set; }
    public bool Crouch { get; set; }
    public bool Run { get; set; }
    public bool DropFlag { get; set; }
    public float TimeStamp { get; set; }

    public static ControlsFrame CollapseFrames(List<ControlsFrame> frames)
    {
        ControlsFrame frame = frames[frames.Count - 1];
        for (int i = frames.Count - 1; i >= 0; i--)
        {
            frame.Horizontal = frames[i].Horizontal;
            frame.Vertical = frames[i].Vertical;
            frame.MouseX = frames[i].MouseX;
            frame.MouseY = frames[i].MouseY;

            if (frames[i].Shoot)
            {
                frame.Shoot = true;
            }
            if (frames[i].Jump)
            {
                frame.Jump = true;
            }
            if (frames[i].DropFlag)
            {
                frame.DropFlag = true;
            }
        }
        frame.Horizontal /= frames.Count;
        frame.Vertical /= frames.Count;
        frame.MouseX /= frames.Count;
        frame.MouseY /= frames.Count;
        return frame;
    }

    public ControlsFrame(float horizontal, float vertical,float mouseX,float mouseY, bool shoot, bool jump, bool crouch, bool run, bool dropFlag, float timeStamp)
    {
        Horizontal = horizontal;
        Vertical = vertical;
        MouseX = mouseX;
        MouseY = mouseY;
        Shoot = shoot;
        Jump = jump;
        Crouch = crouch;
        Run = run;
        DropFlag = dropFlag;
        TimeStamp = timeStamp;
    }
}
