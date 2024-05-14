using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoPersistant : MonoBehaviour
{
#if UNITY_EDITOR
    private void Awake()
    {
        if (SceneManager.GetSceneByBuildIndex(0).name != "Persistent")
        {
            SceneManager.LoadScene("Persistent");
        }
    }
#endif
}
