using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneController : MonoBehaviour
{
    public string sceneName;
    public static ChangeSceneController instance;

    private void Awake()
    {
        if(instance == null) instance = this;
    }

    /// <summary>
    /// Carga la escena cuyo nombre se ha especificado como parametro
    /// </summary>
    /// <param name="nextScene"></param>
    public void ChangeScene(string nextScene)
    {
        //para asegurarnos que no se realicewn cambios
        Time.timeScale = 1;
        //cambiamos a la escena especificada
        //SceneManager.LoadScene(nextScene);
        SceneController.instance.FadeAndLoadScene(sceneName);
        SoundManager.instance.PlayGame();

    }

    /// <summary>
    /// Cierra el juego (solo funcionara ern la build)
    /// </summary>
    public void QuitGame()
    {
#if UNITY_STANDALONE
        //Cierra el juego en la build
        Application.Quit();
#endif

#if UNITY_EDITOR
        //desactiva la ejecucion del proyecto en Unity
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
