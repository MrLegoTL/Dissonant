using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ReactionPlayer : Reaction
{
    public CharacterController player;
    public NavMeshAgent dron;

    protected override void React()
    {
        player.enabled = true;
        dron.enabled = true;
        DataManager.instance.staticPlayer = true;
        DataManager.instance.staticDron = true;
    }
}
