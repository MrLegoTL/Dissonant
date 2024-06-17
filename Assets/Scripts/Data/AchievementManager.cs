using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para realizar consultas a arrays
using System.Linq;
//para hacer uso de los actions
using System;

public class AchievementManager : MonoBehaviour
{
    //Action de desbloqueo de logro que informara del nombre del logro y de su imagen
    public static Action<string, string> OnAchievementUnlock;

    private void OnEnable()
    {
        //suscripcion al metodo de muerte del enemigo mediante el uso de un método anónimo
        //tened en cuenta que este metodo no podra ser DESUSCRITO por lo que debera utilizarse 
        //en clases que sean persistentes en nuestro proyecto

        DataManager.onInteractDron += () => IncreaseStatAndCheckAchiviement("dron", 1);
        DataManager.onInteractPlants += () => IncreaseStatAndCheckAchiviement("plant", 1);
        DataManager.onDimensionTravel += () => IncreaseStatAndCheckAchiviement("portal", 1);
    }

    /// <summary>
    /// Incrementa una estadistica y verifica sus logros asociados
    /// </summary>
    /// <param name="code"></param>
    /// <param name="amount"></param>
    public void IncreaseStatAndCheckAchiviement(string code, int amount)
    {
        //recuperamos la estadistica solicitada, mediante el uso de Linq
        //haciendo una cosulta para recuperar aquella que coincida en el codigo
        Stat stat = DataManager.instance.data.statistics.Where(s => s.code == code).FirstOrDefault();

        //si no existe la estadistica solicitada, no hacemos nada
        if (stat == null) return;
        //incrementamos la estadistica con el valor especificado
        stat.value += amount;

        //recorremos todos los logros que dependen de esta estadistica, que no hayan sido desbloqueada previamente
        foreach (Achievement achievement in
                 DataManager.instance.data.achievements.Where(a=> a.statCode == code && !a.unlocked).AsEnumerable())
        {
            //si con el valor desbloqueamos el logro, lo marcaremos como completado
            //e invocaremos el evento
            if(stat.value >= achievement.targetAmount)
            {
                achievement.unlocked = true;
                //lanzamos el action con la informacion del logro desbloqueado
                OnAchievementUnlock?.Invoke(achievement.name, achievement.imageName);
                //adicionalmente pedimos al datamanager que guarde al desbloquear un logro
                DataManager.instance.Save();
            }
        }
    }
   
}
