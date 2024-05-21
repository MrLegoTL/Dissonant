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
    public float moveTime = 2f;

    

    protected override void React()
    {
        //if (!snapping)
        //{
        //    isMoving = true;
        //    deactivateCollider.SetActive(true);
        //    if (interactable.triggerInteract)
        //    {
        //        player.transform.SetParent(platform.transform);
        //        player.enabled = false;
        //    }

        //    platform.transform.DOMove(finalPosition.position, moveTime).OnComplete(() =>
        //    {
        //        player.enabled = true;
        //        isMoving = false;
        //        player.transform.SetParent(null);
        //    });

        //    snapping = true;
        //}
        //else if (snapping)
        //{
        //    isMoving = true;
        //    deactivateCollider.SetActive(false);
        //    if (interactable.triggerInteract)
        //    {
        //        player.transform.SetParent(platform.transform);
        //        player.enabled = false;
        //    }

        //    platform.transform.DOMove(initialPosition.position, moveTime).OnComplete(() =>
        //    {
        //        isMoving = false;
        //        player.transform.SetParent(null);
        //        player.enabled = true;
        //    });

        //    snapping = false;
        //}

        if (isMoving) return; // Evita que se pueda iniciar otro movimiento mientras la plataforma ya está en movimiento

        isMoving = true;
        deactivateCollider.SetActive(true);

        if (interactable.triggerInteract)
        {
            player.transform.SetParent(platform.transform);
            player.enabled = false;
        }

        Vector3 targetPosition = snapping ? initialPosition.position : finalPosition.position;
        snapping = !snapping;

        platform.transform.DOMove(targetPosition, moveTime).OnComplete(() =>
        {
            player.enabled = true;
            player.transform.SetParent(null);
            isMoving = false;
            deactivateCollider.SetActive(false);
        });



    }

    


}

    

     
