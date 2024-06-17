using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionInteractDron : Reaction
{
    protected override void React()
    {
        DataManager.onInteractDron?.Invoke();
    }

   
}
