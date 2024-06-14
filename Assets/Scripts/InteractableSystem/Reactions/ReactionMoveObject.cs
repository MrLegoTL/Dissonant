using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionMoveObject : Reaction
{
    public GameObject objToMove;
    public float moveY = 2f;
    protected override void React()
    {
        objToMove.transform.DOMoveY(moveY, 1f);
    }
}
