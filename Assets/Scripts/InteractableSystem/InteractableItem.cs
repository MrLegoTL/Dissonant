using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para realizar busquedas en arrays
using System.Linq;


public class InteractableItem : InteractableObjects
{
    public string itemName;

    protected override void Start()
    {
        base.Start();
        if (IsPicked())
        {
            Debug.Log("Cogido");
            //DataManager.instance.SaveObjectPositions(itemName);
        }
    }

    /// <summary>
    /// Verifica si el obejto ya ha sido recogido
    /// </summary>
    /// <returns></returns>
    private bool IsPicked()
    {
        //buscamos el item en el listado de items
        ObjectState result = DataManager.instance.data.objectStates.Where(i => i.objectName == itemName).FirstOrDefault();
        //si existe devuelvo el valor de la booleana
        if (result != null)
        {
            return result.picked;
        }

        Debug.LogWarning("El nombre del item no existe en la lista: " + itemName);
        return false;
    }
}
