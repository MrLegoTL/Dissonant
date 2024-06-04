using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para gestionar la carga de escenas
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //canvas para hacer el oscurecido de pantalla
    public CanvasGroup faderCanvasGroup;
    //duracion del fundido
    public float fadeDuration = 1f;
    //escena inicial al cargar
    public string startScene;
    //para indicar que se esta realizando un fade actualemnte
    private bool isFading;

    
    

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //comenzamos con la pantalla en negro
        faderCanvasGroup.alpha = 1f;
        //cargamos la escena configurada como inicial
        yield return StartCoroutine(LoadSceneAndSetActive(startScene));
        //una vez cargada la escena
        StartCoroutine(Fade(0));

        
        
    }

    /// <summary>
    /// Realiza el fundido hasta el alpha inficado
    /// </summary>
    /// <param name="finalAlpha"></param>
    /// <returns></returns>
    private IEnumerator Fade(float finalAlpha)
    {
        //activamos la animacion de loading cuando se muestra el oscurecedor de pantalla
        //if (finalAlpha == 1) loadingAnimator.SetTrigger("Play");

        //indicamos que al principio de la corrutina se inicia el fade
        isFading = true;
        //bloquemaos raycast para evitar qu ele jugador interactue con nada durante el fade
        faderCanvasGroup.blocksRaycasts = true;
        //alamcenamos el alpha inicial antes del fade
        float initialAlpha = faderCanvasGroup.alpha;
        float timeCounter = fadeDuration;

        while (timeCounter > 0)
        {
            //hacerms un lerp para hacer el fundido de forma gradual
            faderCanvasGroup.alpha = Mathf.Lerp(initialAlpha, finalAlpha, 1f - (timeCounter / fadeDuration));

            timeCounter -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        //cuando se retira el oscurecedor de pantalla, paramos la naimacion de loading
        //if (finalAlpha == 0) loadingAnimator.SetTrigger("Stop");
        //por si el resutlado noes un valor exacto, lo forzamos
        faderCanvasGroup.alpha = finalAlpha;
        //tras el loop, indicamos que el fade ha terminado
        isFading = false;

        //si el objetivo final es mostrar la pantalla de juego, desactivamos el block raycast una vez finalizado
        if (finalAlpha == 0) faderCanvasGroup.blocksRaycasts = false;

    }

    /// <summary>
    /// Carga una escena de forma asincrona y la configura como escena activa
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator LoadSceneAndSetActive(string sceneName)
    {
        
        if (sceneName == "")
        {
            Debug.LogWarning("No se ha indicado una escean que cargar");
            //salimos de la corrutina
            yield break;
        }
        //cargamos la escena de forma asincrona y aditiva, sin destruir las otras escenas que actualmente esten cargadas
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //fuerza la actualizacion de los lightprobes de la escena
        LightProbes.Tetrahedralize();

        //recuperamos la ultima escena cargada
        Scene newlyLoadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        //marcamos esta escena como activa
        SceneManager.SetActiveScene(newlyLoadedScene);

        
    }

    /// <summary>
    /// hace un fade y cambia la escena
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    private IEnumerator FadeAndSwitchScenes(string sceneName)
    {
        //fade out d ela escena, hacemos un yield return para esperar a que termine
        yield return StartCoroutine(Fade(1));
        //una vez finalizado el fade
        //descargamos la escena activa de forma asincrona
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        //una vez termianda la descarga, cargamos y activamos la escean indicada
        yield return StartCoroutine(LoadSceneAndSetActive(sceneName));
        //tras terminar la carga de la escena, realizamos un fade in
        yield return StartCoroutine(Fade(0));        
        
        
    }

    /// <summary>
    /// LLamada publica para el cambio de escena
    /// </summary>
    /// <param name="sceneName"></param>
    public void FadeAndLoadScene(string sceneName)
    {
        //si ya hay un cambio de escena iniciado no haremos nada
        if (isFading) return;
        //iniciamos el proceso de fundido y carga de escena
        StartCoroutine(FadeAndSwitchScenes(sceneName));
        
    }
}
