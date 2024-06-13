using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDropDissonant : Reaction
{

    [Header("Drop")]
    //referencia a la posicion de la mano del jugador
    public Transform dropPoint;
    //el objeto actualmente en la mano del jugador
    public GameObject objectDropped;
    ///public GameObject obj;
    public bool dropped = false;
    public FPSController player;

    private void Start()
    {
        player = FindObjectOfType<FPSController>();
        
        

    }

   
    protected override void React()
    {
        if (!dropped) DropOff();

    }



    public void DropOff()
    {
        
        //DataManager.instance.SaveObjectPositions(obj.name);
        //Coloca el objeto en la posicion de la mano del jugador
         player.handObject.transform.position = dropPoint.position;
         player.handObject.transform.rotation = dropPoint.rotation;

        //Cambia el padre del objeto para que sea hijo de la mano
        player.handObject.transform.parent = dropPoint;

        //Guarda uan referencia al objeto en la mano del jugador
        objectDropped = player.handObject;
        dropped = true;
        DataManager.instance.objectInHandName = "";
    }
}
