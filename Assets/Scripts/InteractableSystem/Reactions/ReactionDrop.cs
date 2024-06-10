using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDrop : Reaction
{
    [Header("Drop")]
    //referencia a la posicion de la mano del jugador
    public Transform dropPoint;
    //el objeto actualmente en la mano del jugador
    public GameObject objectDropped;
    public GameObject obj;
    public bool dropped  = false;
    


    


    

    protected override void React()
    {
       if(!dropped) DropOff();
        
    }

   

    public void DropOff()
    {
        //Coloca el objeto en la posicion de la mano del jugador
        obj.transform.position = dropPoint.position;
        obj.transform.rotation = dropPoint.rotation;

        //Cambia el padre del objeto para que sea hijo de la mano
        obj.transform.parent = dropPoint;

        //Guarda uan referencia al objeto en la mano del jugador
        objectDropped = obj;
        dropped = true;
        DataManager.instance.objectInHandName = "";
    }

  
}
