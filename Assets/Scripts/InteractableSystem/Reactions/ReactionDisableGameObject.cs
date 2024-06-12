using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionDisableGameObject : Reaction
{
    public GameObject targetObject;

    protected override void React()
    {
        ToggleObject();
    }

    public void ToggleObject()
    {
        if (targetObject != null)
        {
            // Cambiar el estado activo del objeto
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);

            
        }
        
    }
}
