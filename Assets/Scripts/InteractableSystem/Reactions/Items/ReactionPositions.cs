using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionPositions : Reaction
{
    public Transform pointPosition;
    public GameObject obj;
    protected override void React()
    {
        obj.transform.position = pointPosition.position; 
    }
}
