using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    //Referencia al Animator del objeto interactuable
    public Animator anim;
   

    public void Interact()
    {
        //verifica si el aniamtor esta asignado
        if (anim != null)
        {
            //Cambia el estado del parametro  booleano en el animator para abrir o cerrar la puerta
            anim.SetBool("character_nearby", !anim.GetBool("character_nearby"));

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("character_nearby", true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("character_nearby", false);
        }
    }

}
