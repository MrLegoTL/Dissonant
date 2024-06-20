using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionSpeedRunner : Reaction
{
    protected override void React()
    {
        DataManager.instance.startedTime = false;
        if(DataManager.instance.timeCounter<= 300f)
        {
            DataManager.onSpeedRunner?.Invoke();
        }
    }
}
