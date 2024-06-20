using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCounterTime : Reaction
{
    protected override void React()
    {
        DataManager.instance.startedTime = true;
    }
}
