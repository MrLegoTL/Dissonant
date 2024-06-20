using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionChaosFix : Reaction
{
    protected override void React()
    {
        DataManager.onChaosFix?.Invoke();
    }
}
