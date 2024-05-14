using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionCondition : Reaction
{
    public string conditionName;

    public bool done ;

    protected override void React()
    {
        done = !done;

        DataManager.instance.SetCondition(conditionName, done);
    }
}
