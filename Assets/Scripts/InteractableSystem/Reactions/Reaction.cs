using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaction : MonoBehaviour
{

    [TextArea]
    //descripcion d ela reaccion, como una notra para el editor
    public string description;
    //timepo que tardara en ejecutarse esta reaccion
    public float delay;
    //contador interno para gestionar el delay
    private float delayCounter;

    //referencia al interactable, que dejaremos oculta ya que se configurara automaticamente desde otro script
    [HideInInspector]
    public InteractableObjects interactable;

    /// <summary>
    /// Acción a realizar antes del delay
    /// </summary>
    protected virtual void React()
    {

    }

    /// <summary>
    /// Accion a realizar despues del delay
    /// </summary>
    protected virtual void PostReact()
    {
        //solicitamos pasar a la siguiente reaccion
        interactable.NextReaction();
    }

    /// <summary>
    /// Corrutina que sera ejecutada como reacción
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator DelayReact()
    {
        //realizamos las acciones de la reaccion
        React();

        //reiniciamos el contador de tiempo
        delayCounter = delay;

        //realizamos la espera por el tiempo indicado
        while (delayCounter > 0)
        {
            yield return new WaitForEndOfFrame();
            delayCounter -= Time.deltaTime;
        }

        //una vez finalizado el delay, realizaremos las acciones de final de reaccion
        PostReact();
    }

    /// <summary>
    /// Metoddo que iniciara la ejecucion de la reacción
    /// </summary>
    public virtual void ExecuteReaction()
    {
        StartCoroutine(DelayReact());
    }

}
