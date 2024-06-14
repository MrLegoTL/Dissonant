using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionLightsPortal : Reaction
{
    public GameObject lightPortal;
    public GameObject generalLightsPortals;
   


    protected override void React()
    {
        lightPortal.SetActive(true);
        generalLightsPortals.SetActive(true);
        
    }
}
