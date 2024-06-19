using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControlles : MonoBehaviour
{
    public CanvasGroup group;
    public void SetCanvasGroupActive(bool enable)
    {
        //hacemos visible o invisible el canvas
        group.alpha = enable ? 1 : 0;
        //activamos la interaccion del canvas
        group.interactable = enable;
        //permitimos que el canvas bloquee raycast
        group.blocksRaycasts = enable;
    }
}
