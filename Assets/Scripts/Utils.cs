using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils 
{
    /// <summary>
    /// Hace visible o invisible el canvas group indicado
    /// </summary>
    /// <param name="group"></param>
    /// <param name="enable"></param>
    public static void ActiveCanvasGroup(CanvasGroup group, bool enable)
    {
        //hacemos visible o invisible el canvas
        group.alpha = enable ? 1 : 0;
        //activamos la interaccion del canvas
        group.interactable = enable;
        //permitimos que el canvas bloquee raycast
        group.blocksRaycasts = enable;
    }
}
