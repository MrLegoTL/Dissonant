
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionAnimation : Reaction
{
    //animator del objeto que sera animado
    public Animator target;
    //nombre del trigger del animator
    public string triggerName;


    protected override void React()
    {
        //ejecuto el trigger en el animator especificado
        target.SetTrigger(triggerName);
    }
}
