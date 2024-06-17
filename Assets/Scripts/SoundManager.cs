using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para poder hacer uso de los audio mixers
using UnityEngine.Audio;
using UnityEngine.UI;

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

    public AudioMixer audioMixer;
    public Slider musicVolumeSlider; // Slider para ajustar el volumen de la música
    public Slider soundEffectsSlider; // Slider para ajustar el volumen de los efectos sonoros


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
            //esto hara que la instancia no sea destruida entre escenas
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // en caso de que ya exista una instancia, para evitar solapamientos, autodestruiremos la nueva instancia
            Destroy(gameObject);
        }

        
        
    }

    void Start()
    {
        // Asegurarse de que los sliders estén asignados correctamente en el inspector
        if (musicVolumeSlider == null || soundEffectsSlider == null)
        {
            Debug.LogError("Los sliders no están asignados en el inspector.");
            return;
        }
        
        UpdateSliders();
    }

  

  
    /// <summary>
    /// Reproduce el audio de menu
    /// </summary>
    [ContextMenu("Play Menu Music")]
    public void PlayMainMenu()
    {
        if (audioSource.clip == menuClip) return;

        ChangeMusic(menuClip);
    }

    /// <summary>
    /// Reproduce el audio de game
    /// </summary>
    [ContextMenu("Play Game Music")]
    public void PlayGame()
    {
        if (audioSource.clip == gameClip) return;

        ChangeMusic(gameClip);
    }
    /// <summary>
    /// Reproduce el audio del Mundo de Amateratsu
    /// </summary>
    [ContextMenu("Play Amateratsu Music")]
    public void WorldAmateratsu()
    {
        if(audioSource.clip == amateratsuClip) return;
        ChangeMusic(amateratsuClip);

    }

    public void WorldBlossom()
    {
        if (audioSource.clip == blossomClip) return;
        ChangeMusic(blossomClip);
    }

    private void ChangeMusic(AudioClip clip)
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeAndChangeClip(clip, musicVolumeSlider.value));
    }

    /// <summary>
    /// Hac un cambio de clip suavizando el cambio mediante una variacion del volumen
    /// </summary>
    /// <param name="clip"></param>
    /// <returns></returns>
    private IEnumerator FadeAndChangeClip(AudioClip clip, float volume)
    {

        // usaremos el contador con la mitad del tiempo ya que deberemos hacer el fundido de salida y de entrada
        float counter = fadeTime/2;

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
            audioSource.volume = (counter/(fadeTime/2))*volume;
            counter += Time.deltaTime;
            yield return null;
        }
        audioSource.volume = volume;
    }


    /// <summary>
    /// Configura el volumen del canal Sounds
    /// </summary>
    /// <param name="volume"></param>
    public void SetSound(float volume)
    {
        audioMixer.SetFloat("Sounds", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Sounds", volume);
    }

    /// <summary>
    /// configura el volumen de la musica
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music", volume);
    }

    // Método para manejar el cambio de valor del slider de música
    public void OnMusicVolumeChanged()
    {
        SetMusic(musicVolumeSlider.value);
    }

    // Método para manejar el cambio de valor del slider de efectos sonoros
    public void OnSoundEffectsVolumeChanged()
    {
        SetSound(soundEffectsSlider.value);
    }

    private void UpdateSliders()
    {
        // Verificar que los sliders no sean nulos antes de actualizar sus valores
        if (musicVolumeSlider != null)
        {
            // Actualizar el valor del slider de música con el volumen actual del AudioMixer
            float musicVolume;
            audioMixer.GetFloat("Music", out musicVolume);
            musicVolumeSlider.value = Mathf.Pow(10f, musicVolume / 20f);
        }

        if (soundEffectsSlider != null)
        {
            // Actualizar el valor del slider de efectos de sonido con el volumen actual del AudioMixer
            float soundVolume;
            audioMixer.GetFloat("Sounds", out soundVolume);
            soundEffectsSlider.value = Mathf.Pow(10f, soundVolume / 20f);
        }
    }


}
