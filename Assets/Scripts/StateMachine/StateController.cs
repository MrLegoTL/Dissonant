using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    //estado actual (y a la vez estado de inicio)
    public State currentState;
    //referecnia al animator del controller
    public Animator animator;
    //referencia al navmeshAgent
    public NavMeshAgent navMeshAgent;
    //listado de wayPoints disponibles para la patrulla
    public List<Transform> wayPointsList;
    //siguiente waypoint a desplazarse
    public int nextWayPoint;
    //para desactivar la IA en caso de que sea necesario
    public bool aiActive = true;

    [Header("DetectPlayer")]
    public float detectionRadius = 5f;
    public float raycastMaxDistance = 2f;
    public float offsetDistance = 1f;
    public LayerMask detectionLayer;

    

    private void Start()
    {
        if (DataManager.instance.staticDron)
        {
            navMeshAgent.enabled = true;
            aiActive = true;
            animator.SetTrigger("WakeUp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //si la ia no esta activa no haremos nada
        if (!aiActive) return;
        //ejecutamos el update del state, indicando que sera etse controller a utilizar
        currentState.UpdateState(this);
        DetectPlayer();
    }

   

    void DetectPlayer()
    {
        // Posici�n del objeto desde donde se lanza el SphereCast
        Vector3 origin = transform.position + transform.forward  * offsetDistance;

        // Realiza el SphereCast
        RaycastHit[] hits = Physics.SphereCastAll(origin, detectionRadius, transform.forward, raycastMaxDistance, detectionLayer);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Si se detecta al jugador, desactiva el NavMeshAgent
               navMeshAgent.isStopped = true;
                Debug.Log("Jugador detectado, NavMeshAgent desactivado.");
                return;
            }
            
        }

        // Si no se detecta al jugador, activa el NavMeshAgent
        navMeshAgent.isStopped = false; ;
    }
    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + transform.forward * offsetDistance;
        // Dibujar el SphereCast en la escena para visualizaci�n
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, detectionRadius);
        Gizmos.DrawLine(origin, origin + transform.forward * raycastMaxDistance);
    }
}
