using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "AISystem/Actions/Patrol")]
public class PatrolActions : StateAction
{
    //booleana para indicar si la patrulla sera aleatoria
    public bool randomPatrol = false;

    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    /// <summary>
    /// Realiza las acciones de cambio de wayPoint y gestions del navmesh para la patrulla
    /// </summary>
    /// <param name="controller"></param>
    private void Patrol(StateController controller)
    {
        //recuperamos el siguiente destino de la lista de wayPoints
        controller.navMeshAgent.destination = controller.wayPointsList[controller.nextWayPoint].position;
        //hacemos que se mueva
        controller.navMeshAgent.isStopped = false;

        //si la distancia restante al destino es inferior al stoppingDistance, y no hay ningun camino pendiente
        //por calcular, consideramos que hemos llegado al destino y seleccionaremos un nuevo waypoint
        if(controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending)
        {
            //si hemos marcado con aleatoriedad la patrulla
            if (randomPatrol)
            {
                //elegimos el punto aleatoriamente
                controller.nextWayPoint = Random.Range(0,controller.wayPointsList.Count);
            }
            else
            {
                //en caso contrario elegimos el siguiente waypoint de la lista
                //haciendo el modulo, nos aseguramos de qu eno se desborde el indice de la lista al estar utilizando
                //el resto de la operacion
                controller.nextWayPoint  =(controller.nextWayPoint +1) % controller.wayPointsList.Count;
            }
        }
    }
}
