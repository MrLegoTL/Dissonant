using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionItemCondition : Reaction
{
    public string conditionName;

    public bool done;

    protected override void React()
    {
        done = !done;

        DataManager.instance.SetItemCondition(conditionName, done);
    }

}
