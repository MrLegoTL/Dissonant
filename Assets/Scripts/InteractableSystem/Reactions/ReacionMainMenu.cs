using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReacionMainMenu : Reaction
{
    public string sceneName;
    protected override void React()
    {
        ChangeSceneController.instance.ChangeScene(sceneName);
        //hacemos visible el cursor
        Cursor.visible = true;
        //desbloqueamos el cursor
        Cursor.lockState = CursorLockMode.None;
    }
}
