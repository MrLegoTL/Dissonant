using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionToggleDisableGameObjects : Reaction
{
    public GameObject[] targetObjects;

    protected override void React()
    {
        ToggleObjects();
    }
    public void ToggleObjects()
    {
        if (targetObjects != null)
        {
            foreach (GameObject targetObject in targetObjects)
            {
                if (targetObject != null)
                {
                    // Cambiar el estado activo del objeto
                    bool isActive = targetObject.activeSelf;
                    targetObject.SetActive(!isActive);
                }
            }
        }
    }
}
