using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionPlatform : Reaction
{
    public GameObject platform;
    public Vector3 initialPosition;
    public Vector3 finalPosition;
    public bool snapping = false;
    public bool isMoving = false;
    public GameObject deactivateCollider;
    public FPSController player;
    public float moveTime = 2f;
    public float moveDistanceX = 10f;
    


    private void Start()
    {
        // Guarda la posición inicial de la plataforma
        initialPosition = platform.transform.localPosition;
        // Calcula la posición final de la plataforma desplazándose en el eje X
        finalPosition = initialPosition + new Vector3(moveDistanceX,0,0);
    }


    protected override void React()
    {
        if (isMoving) return; // Evita que se pueda iniciar otro movimiento mientras la plataforma ya está en movimiento

        isMoving = true;
        deactivateCollider.SetActive(true);

        if (interactable.triggerInteract)
        {
            player.transform.SetParent(platform.transform);
            player.enabled = false;
        }

        Vector3 targetPosition = snapping ? initialPosition : finalPosition;
        snapping = !snapping;

        platform.transform.DOLocalMoveX(targetPosition.x, moveTime).OnComplete(() =>
        {
            player.enabled = true;
            player.transform.SetParent(null);
            isMoving = false;
            deactivateCollider.SetActive(false);
        });
    }
}
