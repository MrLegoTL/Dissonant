using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionPlayer : Reaction
{
    public CharacterController player;

    protected override void React()
    {
        player.enabled = true;
    }
}
