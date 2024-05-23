using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
    }

    /// <summary>
    /// Bloquea y oculta el cursor
    /// </summary>
    public static void LockCursor()
    {
        //ocultamos el cursor
        Cursor.visible = false;
        //bloqueamos el cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Libera y hace visible el cursor
    /// </summary>
    public static void ReleaseCursor()
    {
        //hacemos visible el cursor
        Cursor.visible = true;
        //desbloqueamos el cursor
        Cursor.lockState = CursorLockMode.None;
    }
}
