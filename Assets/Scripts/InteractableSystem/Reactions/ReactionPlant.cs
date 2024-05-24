using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionPlant : Reaction
{
    public GameObject plant;
    public float moveTime = 3f;

   
    protected override void React()
    {
        plant.transform.DOMoveY(26f, moveTime);
        DestroyPlant();
        
    }

    public void DestroyPlant()
    {
        StartCoroutine(WaitToDestroyPlant());
    }

    private IEnumerator WaitToDestroyPlant()
    {
        while(plant.transform.position.y <= 25)
        {
            yield return new WaitForEndOfFrame();
        }

        plant.SetActive(false);


    }
    
}
