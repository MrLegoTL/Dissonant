using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionPickUp : Reaction
{
    [Header("PickUp")]
    //referencia a la posicion de la mano del jugador
    
    
    public GameObject obj;

    protected override void React()
    {
        PickUp();
    }

    public void PickUp()
    {
        if (DataManager.instance.objectInHandName != "") return;
        //Coloca el objeto en la posicion de la mano del jugador
        obj.transform.position = FPSController.instance.handToObject.position;
        //obj.transform.rotation = hand.rotation;

        //Cambia el padre del objeto para que sea hijo de la mano
        obj.transform.parent = FPSController.instance.handToObject;

        //Guarda uan referencia al objeto en la mano del jugador
        DataManager.instance.objectInHandName = obj.name;
    }
}

