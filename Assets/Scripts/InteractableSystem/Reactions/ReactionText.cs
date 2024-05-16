using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionText : Reaction
{
    //ID del texto a mostrar
    public string textID;
    //color del texto a mostrar
    public Color textColor = Color.black;
    //delay por cada caracter que tenga el texto a mostrar
    public float characterReadTime = 0.05f;
    //action que proporcionara el mensaje y el color a mostrar
    public static Action<string, Color> OnDisplayMessage;
    //action que informara que se oculta el mensaje
    public static Action OnHideMessage;

    protected override void React()
    {
        //mostramos el texto y activamos el panel
        //textContainer.DisplayMessage(text, textColor);
        OnDisplayMessage?.Invoke(TranslateManager.instance.GetString(textID), textColor);

        //si el delay es 0, usamos la longuitud dle texto para calcular el tiempo de Delay
        //en caso contrario respetamos el tiempo asignado
        delay = (delay == 0) ? TranslateManager.instance.GetString(textID).Length * characterReadTime : delay;

       
    }

    protected override void PostReact()
    {
        //ocultamos el mensaje tras el delay, antes de pasar a la siguiente reaccion
        //textContainer.HideMessage();
        OnHideMessage?.Invoke();

        base.PostReact();
    }
}
