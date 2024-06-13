using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionPositionDisonant : Reaction
{
    public Transform pointPosition;
    public GameObject obj;
    protected override void React()
    {
        PositionObjectInDissonantPlatform();
    }
    public void PositionObjectInDissonantPlatform()
    {
        //if (DataManager.instance.CheckObjectInHand() == null) return;
        GameObject temp = Instantiate(obj, pointPosition.position, obj.transform.rotation);
        temp.transform.parent = pointPosition;

      

    }
}
