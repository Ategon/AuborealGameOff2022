using System.Collections;
using System.Collections.Generic;
using Assets.EventSystem;
using UnityEngine;

public class FootstepEventParameters : EventParameters
{
    public string labelName;
    public string labelValue;

    public FootstepEventParameters(string labelName, string labelValue)
    {
        this.labelName = labelName;
        this.labelValue = labelValue;
    }
}
