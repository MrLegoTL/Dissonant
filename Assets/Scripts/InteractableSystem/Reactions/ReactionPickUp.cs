using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReactionPickUp : Reaction
{
    [Header("PickUp")]
    //referencia a la posicion de la mano del jugador   
    public Transform handPlayer;
    public InteractableItem currentItem;
    public GameObject obj;


    private void Start()
    {
        currentItem  = GetComponentInParent<InteractableItem>();
    }

    public void PickUp()
    {
        if (DataManager.instance.objectInHandName != "") return;
        //    if (DataManager.instance.objectInHandName != "") return;

        //buscamos el item en el listado de items
        ObjectState result = DataManager.instance.data.objectStates.Where(i => i.objectName == currentItem.itemName).FirstOrDefault();

        //si no encontramos el objeto en la lista abandonamos el metodo
        if (result == null)
        {
            Debug.LogWarning("El nombre del objeto buscado no existe: " + currentItem.itemName);
            return;
        }       
        //DataManager.instance.SaveObjectPositions(currentItem.itemName);

        result.picked = true;

        //    currentItem.itemName = DataManager.instance.objectInHandName;
        //    //Coloca el objeto en la posicion de la mano del jugador
        //    obj.transform.position = handPlayer.position;
        //    //obj.transform.rotation = hand.rotation;

        //    //Cambia el padre del objeto para que sea hijo de la mano
        //    obj.transform.parent = handPlayer;

        //    //Guarda uan referencia al objeto en la mano del jugador
        //    DataManager.instance.objectInHandName = obj.name;
        //}
        //Coloca el objeto en la posicion de la mano del jugador
        obj.transform.position = handPlayer.position;
        //obj.transform.rotation = hand.rotation;

        //Cambia el padre del objeto para que sea hijo de la mano
        obj.transform.parent = handPlayer;

        //Guarda uan referencia al objeto en la mano del jugador
        DataManager.instance.objectInHandName = obj.name;
    }
    protected override void React()
    {
        PickUp();
    }

   
}

