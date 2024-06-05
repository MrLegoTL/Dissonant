using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="AISystem/State")]
public class State : ScriptableObject
{
    //acciones que sera realizadas en el Update del state
    public StateAction[] updateAction;

    /// <summary>
    /// Ejecuta la lista de actions recibida como parametro
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="actions"></param>
    private void DoAction(StateController controller, StateAction[] actions)
    {
        //recorremos todas las acciones configuradas
        for(int i = 0; i < actions.Length; i++)
        {
            //y las ejecutamos
            actions[i].Act(controller);
        }
    }

    /// <summary>
    /// Acciones de Update State
    /// </summary>
    /// <param name="controller"></param>
    public void UpdateState(StateController controller)
    {
        DoAction(controller, updateAction);
    }
}
