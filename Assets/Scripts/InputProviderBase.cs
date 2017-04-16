using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputProviderBase : MonoBehaviour
{
    public abstract ControlsFrame Controls{ get; } 
}
