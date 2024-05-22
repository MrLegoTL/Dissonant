using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTranform : Reaction
{
    public GameObject obj;
    public float objTranform;

    protected override void React()
    {
        obj.transform.localScale = new Vector3(objTranform,objTranform,objTranform);
    }
}
