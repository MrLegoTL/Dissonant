using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionInteractProp : Reaction
{
    protected override void React()
    {
        DataManager.onInteractProp?.Invoke();
    }
}
