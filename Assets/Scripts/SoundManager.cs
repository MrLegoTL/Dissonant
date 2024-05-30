using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    //Clip de sonido que sera reproducido en el Menu principal
    public AudioClip menuClip;
    //clip de sonido que sera reproducido en la base espacial
    public AudioClip gameClip;
    //clip de sonido que sera reproducido en el mundo 1 (Amateratsu)
    public AudioClip amateratsuClip;
    //clip de sonido que sera repreoducido en el mundo 2 (Blossom)
    public AudioClip blossomClip;
    //clip de sonido que sera reproducido al final del juego
    public AudioClip finalClip;
    //timepo de fundido entre pistas de audio
    [Range(1, 3)]
    public float fadeTime = 2f;
    //timepo de acmbio de pitch
    [Range(0, 2)]
    public float pitchTime = 1f;


    //corrutina para la gestion del fade entre musicas
    private Coroutine fadeCoroutine;
    //corrutina para el cambio del pitch
    private Coroutine pitchCoroutine;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // en caso de que ya exista una instancia, para evitar solapamientos, autodestruiremos la nueva instancia
            Destroy(gameObject);
        }

        //esto hara que la instancia no sea destruida entre escenas
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Reproduce el audio de menu
    /// </summary>
    [ContextMenu("Play Menu Music")]
    public void PlayMainMenu()
    {
        if (audioSource.clip == menuClip) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAndChangeClip(menuClip));
    }

    /// <summary>
    /// Reproduce el audio de game
    /// </summary>
    [ContextMenu("Play Game Music")]
    public void PlayGame()
    {
        if (audioSource.clip == gameClip) return;

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAndChangeClip(gameClip));
    }
    /// <summary>
    /// Reproduce el audio del Mundo de Amateratsu
    /// </summary>
    [ContextMenu("Play Amateratsu Music")]
    public void WorldAmateratsu()
    {
        if(audioSource.clip == amateratsuClip) return;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAndChangeClip(amateratsuClip));

    }

    public void WorldBlossom()
    {
        if (audioSource.clip == blossomClip) return;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAndChangeClip(blossomClip));
    }

    /// <summary>
    /// Hac un cambio de clip suavizando el cambio mediante una variacion del volumen
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    private IEnumerator FadeAndChangeClip(AudioClip clip)
    {
        // usaremos el contador con la mitad del tiempo ya que deberemos hacer el fundido de salida y de entrada
        float counter = audioSource.volume;

        while (counter > 0)
        {
            ////vamos reduciendo el volumen
            audioSource.volume = counter / (fadeTime / 2);
            //reducimos el contador
            counter -= Time.deltaTime;
            yield return null;
        }

        //relizamos el cambio de clip al que recibimos como parametro
        audioSource.clip = clip;
        //Iniciamos la reproduccion ya que el cambio de clip la detiene
        audioSource.Play();


        while (counter < (fadeTime / 2))
        {
            audioSource.volume = 0.2f;
            counter += Time.deltaTime;
            yield return null;
        }
    }
   
}
