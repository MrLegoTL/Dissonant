using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeteleButton : MonoBehaviour
{
    public void Delete()
    {
        DataManager.instance.DeleteSaveFile();

        //Borrar datos de logros.
        DataManager.instance.data.achievements[0].unlocked = false;
        DataManager.instance.data.achievements[1].unlocked = false;
        DataManager.instance.data.achievements[2].unlocked = false;
        DataManager.instance.data.achievements[3].unlocked = false;
        DataManager.instance.data.achievements[4].unlocked = false;

        //Borrar datos de estadisticas de  logros.
        DataManager.instance.data.statistics[0].value = 0;
        DataManager.instance.data.statistics[1].value = 0;
        DataManager.instance.data.statistics[2].value = 0;
        DataManager.instance.data.statistics[3].value = 0;
        DataManager.instance.data.statistics[4].value = 0;



        //Borrar datos de condiciones.
        DataManager.instance.data.allCondition[0].done = false;
        DataManager.instance.data.allCondition[1].done = false;
        DataManager.instance.data.allCondition[2].done = false;
        DataManager.instance.data.allCondition[3].done = false;
        DataManager.instance.data.allCondition[4].done = false;
        DataManager.instance.data.allCondition[5].done = false;
        DataManager.instance.data.allCondition[6].done = false;
        DataManager.instance.data.allCondition[7].done = false;
        DataManager.instance.data.allCondition[8].done = false;
        DataManager.instance.data.allCondition[9].done = false;
        DataManager.instance.data.allCondition[10].done = false;
        DataManager.instance.data.allCondition[11].done = false;
        DataManager.instance.data.allCondition[12].done = false;
        DataManager.instance.data.allCondition[13].done = false;
        DataManager.instance.data.allCondition[14].done = false;
        DataManager.instance.data.allCondition[15].done = false;
        DataManager.instance.data.allCondition[16].done = false;

        //Borrar datos de condiciones de objeto.
        DataManager.instance.data.allItemCondition[0].done = false;
        DataManager.instance.data.allItemCondition[1].done = false;
        DataManager.instance.data.allItemCondition[2].done = false;
        DataManager.instance.data.allItemCondition[3].done = false;
        DataManager.instance.data.allItemCondition[4].done = false;
        DataManager.instance.data.allItemCondition[5].done = false;
        DataManager.instance.data.allItemCondition[6].done = false;
        DataManager.instance.data.allItemCondition[7].done = false;
        DataManager.instance.data.allItemCondition[8].done = false;

        DataManager.instance.startedTime = false;
        DataManager.instance.timeCounter = 0f;
        DataManager.instance.staticDron = false;
        DataManager.instance.staticPlayer = false;


    }
}
