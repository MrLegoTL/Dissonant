using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionInteractPlantAchiviement : Reaction
{
    protected override void React()
    {
        DataManager.onInteractPlants?.Invoke();
    }

}
