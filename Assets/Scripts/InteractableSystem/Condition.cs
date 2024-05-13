using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Condition 
{
    //nombre d ela condicion, debera ser unico para que funciones correctamente
    public string name;
    //descripcion de apoyo
    public string description;
    //booleana para controlar si se ha cumplido la condicion
    public bool done;
}
