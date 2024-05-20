using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionMovePlatform : Reaction
{
    public GameObject platform;
    public Transform initialPosition;
    public Transform finalPosition;
    public bool snapping = false;
    public bool isMoving = false;
    public GameObject deactivateCollider;
    public FPSController player;
    //private void Awake()
    //{
    //    //DOTween.Init();
    //}

    protected override void React()
    {
        if (!snapping)
        {
            isMoving = true;
            deactivateCollider.SetActive(true);
            if (interactable.triggerInteract)
            {
                player.transform.SetParent(platform.transform);
            }           

            platform.transform.DOMove(finalPosition.position, 1.5f).OnComplete(() =>
            {
                isMoving = false;
                player.transform.SetParent(null);
            });

            snapping = true;
        }
        else if (snapping)
        {
            isMoving = true;
            deactivateCollider.SetActive(false);
            if (interactable.triggerInteract)
            {
                player.transform.SetParent(platform.transform);
            }

            platform.transform.DOMove(initialPosition.position, 1.5f).OnComplete(() =>
            {
                isMoving = false;
                player.transform.SetParent(null);
            });

            snapping = false;
        }

    }

   
}

    

     
