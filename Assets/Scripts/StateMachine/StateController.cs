using System.Collections;
using System.Collections.Generic;
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
    

    // Update is called once per frame
    void Update()
    {
        //si la ia no esta activa no haremos nada
        if (!aiActive) return;
        //ejecutamos el update del state, indicando que sera etse controller a utilizar
        currentState.UpdateState(this);
    }
}
