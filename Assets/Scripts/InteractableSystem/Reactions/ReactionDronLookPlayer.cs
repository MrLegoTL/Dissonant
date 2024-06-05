using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class ReactionDronLookPlayer : Reaction
{
    public Transform player;
    public GameObject dron;
    public StateController controller;
    public NavMeshAgent agent;
    public float timeToRotate = 1f;

    protected override void React()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        agent.isStopped = true;
        controller.aiActive = false;
        
        ////Calcula la diureccion del jugador
        Vector3 directionToPlayer = player.position - transform.position;
        ////Crea una rotacion que haga que el NPC mire hacia el jugador
        Quaternion rotation = Quaternion.LookRotation(directionToPlayer);

        dron.transform.DORotate(directionToPlayer, timeToRotate);
        ////aplica la rotacion al NPC
        //dron.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, timeToRotate);
    }
}
