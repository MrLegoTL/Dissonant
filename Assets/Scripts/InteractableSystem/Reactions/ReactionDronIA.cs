using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDronIA : Reaction
{
    public StateController controller;
    protected override void React()
    {
        controller.aiActive = true;
    }
}
