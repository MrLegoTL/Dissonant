using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SphereCollider))]
public class InteractableObjects : MonoBehaviour
{
    [Header("Conditions")]
    //condiciones que se tienen que cumplir para que se cumpla las acciones positivas
    public string[] conditions;
    public string[] itemConditions;
    public SphereCollider sphereCollider;
    //transform que contendrá como hijos, todas las reacciones positivas
    private Transform positiveReactions;
    //trnaform contendra como hijos, todas las reccioneas por defecto a realizar cuando no se cumplan las condiciones
    private Transform defaultReactions;

    private Transform itemReactions;
    //cola para gestionar las secuecias de reacciones a realizar
    private Queue<Reaction> reactionQueue = new Queue<Reaction>();
    //para saber cuando se esta llevando a cabo la secuencia de reacciones
    private bool reacting = false;
    //indica si elinteractable se disparara cuando el jugador entre en contacto con el
    public bool triggerInteract;

    protected virtual void Start()
    {
        sphereCollider.isTrigger = true;
        //recuperamos de la jerarquia el hijo llamado PositiveReaction (CUIDADO CON ESTO QUE ES HARDCODE)   
        positiveReactions = transform.Find("PositiveReactions");
        //recuperamos de la jerarquia el hijo llamado DefaultReaction (CUIDADO CON ESTO QUE ES HARDCODE)
        defaultReactions = transform.Find("DefaultReactions");
        itemReactions = transform.Find("ItemReactions");
        Items();
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && triggerInteract)
        {
            Interact();
            
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && triggerInteract && this.CompareTag("Door"))
        {
            Interact();
        }
    }

    public void Interact()
    {
        ////verifica si el aniamtor esta asignado
        //if (anim != null)
        //{
        //    //Cambia el estado del parametro  booleano en el animator para abrir o cerrar la puerta
        //    anim.SetBool("character_nearby", !anim.GetBool("character_nearby"));
        //    anim.SetBool("CapsuleOpen", !anim.GetBool("CapsuleOpen"));


        //}

        //si ya se ha iniciado la interaccion, no permitira volver a hacer anda hasta que esta termine
        if (reacting) return;
        Debug.Log("Interact");
        //indicamos que se inicia la secuencia
        reacting = true;

        //por defecto marcamos como umplidad las condiciones
        bool success = true;

        //recorro todas las condiciones
        foreach(string condition in conditions)
        {
            //comprueblo si no se cumple la condicion
            if (!DataManager.instance.CheckCondition(condition))
            {
                //marco la booleana de control como false
                success = false;
                //y salgo del bucle
                break;
            }
        }
         
       
        //si se cumple la condicion y ademas el numero de condiciones es mayor que 0
        if(success && conditions.Length > 0 )
        {
            //ponemos en cola (temporalmente) las reacciones positivas
            QueueReactions(positiveReactions);
        }
        else
        {
            //ponemos en cola (temporalmente) las reacciones por defecto
            QueueReactions(defaultReactions);
        }                 

        //iniciamos la cadena de reacciones
        NextReaction();

    }
    [ContextMenu("Items")]
    public void Items()
    {
        //por defecto marcamos como umplidad las condiciones
        bool done = true;

        //recorro todas las condiciones
        foreach (string condition in itemConditions)
        {
            //comprueblo si no se cumple la condicion
            if (!DataManager.instance.CheckItemCondition(condition))
            {
                //marco la booleana de control como false
                done = false;
                //y salgo del bucle
                break;
            }
        }

        //si se cumple la condicion y ademas el numero de condiciones es mayor que 0
        if (done && itemConditions.Length > 0)
        {
            //ponemos en cola (temporalmente) las reacciones positivas
            QueueReactions(itemReactions);
        }
        //iniciamos la cadena de reacciones
        NextReaction();
        return;
    }
    /// <summary>
    /// Pone en cola las reacciones qu ehayan sido configuradas en el contenedor proporcionado
    /// </summary>
    /// <param name="reactionContainer"></param>
    private void QueueReactions(Transform reactionContainer)
    {
        //limpiamos las reacciones previas que pudieran quedar por ejecutar
        reactionQueue.Clear();

        //recorremos todas las reacciones configuradas y las ponemos en cola
        foreach (Reaction reaction in reactionContainer.GetComponentsInChildren<Reaction>())
        {
            //le indicamos quien es su interactable
            reaction.interactable = this;
            //agregamos el reaction que acabamos de configurar, dentro de la cola
            reactionQueue.Enqueue(reaction);
        }
    }

    /// <summary>
    /// inicia la siguiente interaccion en la cola
    /// </summary>
    public void NextReaction()
    {
        //si aun queda reacciones en la cola
        if (reactionQueue.Count > 0)
        {
            //extraemos el siguiente reaction de la cola y lo ejecutamos
            reactionQueue.Dequeue().ExecuteReaction();
        }
        else
        {
            //si hemos llegado al final, indicamos que se ha terminado la cola de  reacciones
            reacting = false;
        }
    }



}
