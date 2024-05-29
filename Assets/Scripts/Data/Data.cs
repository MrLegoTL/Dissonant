using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data 
{
    //escena actual
    public string currentScene;
    //punto de entrada de la escena
    public string entrancePoint;
    //array con el estado de todas las condiciones
    public Condition[] allCondition;

    //listado de estadisticas disponibles
    public Stat[] statistics;
    //listado de logro disponibles
    public Achievement[] achievements;
}
