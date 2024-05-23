using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionPlatformY : Reaction
{
    public GameObject platform;
    public Vector3 initialPosition;
    public Vector3 finalPosition;
    public Vector3 midPosition;
    public bool snapping = false;
    public bool isMoving = false;
    public GameObject deactivateCollider;
    public FPSController player;
    public float moveTime = 2f;
    public float moveDistanceY = 10f;



    private void Start()
    {
        // Guarda la posición inicial de la plataforma
        initialPosition = platform.transform.localPosition;
        // Calcula la posición final de la plataforma desplazándose en el eje X
        finalPosition = initialPosition + new Vector3(0, moveDistanceY, 0);
        midPosition = initialPosition + new Vector3(0,-moveDistanceY, 0);
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

        Vector3 targetPosition = snapping ? midPosition : finalPosition;
        snapping = !snapping;

        platform.transform.DOLocalMoveY(targetPosition.y, moveTime).OnComplete(() =>
        {
            player.enabled = true;
            player.transform.SetParent(null);
            isMoving = false;
            deactivateCollider.SetActive(false);
        });
    }
}
