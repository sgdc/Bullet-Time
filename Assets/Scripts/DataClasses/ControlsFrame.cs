using System;
using System.Collections.Generic;

public class ControlsFrame
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public bool Shoot { get; set; }
    public bool Jump { get; set; }
    public bool Crouch { get; set; }
    public bool Run { get; set; }

    public ControlsFrame()
    {

    }

    public ControlsFrame(float horizontal, float vertical, bool shoot, bool jump, bool crouch, bool run)
    {
        Horizontal = horizontal;
        Vertical = vertical;
        Shoot = shoot;
        Jump = jump;
        Crouch = crouch;
        Run = run;
    }
}
