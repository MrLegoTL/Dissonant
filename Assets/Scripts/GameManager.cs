using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Game,
        Paused,
        EndGame
        
    }
    [Header("PauseMenu")]
    public CanvasGroup pauseMenu;

    //Alamcena el esatdo actual del juego
    public GameState currentState;
    //Almacena el estado previo del juego
    public GameState previousState;

    //configuramos la clase como singlenton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        currentState = GameState.Game;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckForPauseAndResume();
        }
    }

    public void ChangeMusicScenes(string scene)
    {
        if(scene == "Game")
        {
            SoundManager.instance.PlayGame();
        }
        else if(scene == "World1")
        {
            SoundManager.instance.WorldAmateratsu();
        }
        else if(scene == "World2")
        {
            SoundManager.instance.WorldBlossom();
        }
    }

    /// <summary>
    /// Metodo para cambia el estado del juego
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    /// <summary>
    /// Metodo para la pausa del juego
    /// </summary>
    public void PauseGame()
    {
        //CAMBIA EL ESTADO DEL JUEGO
        ChangeState(GameState.Paused);
        
        //pausa el juego
        Time.timeScale = 0f;
        //activa la pantalla de pausa
        pauseMenu.alpha = 1f;

        //hacemos visible el cursor
        Cursor.visible = true;
        //desbloqueamos el cursor
        Cursor.lockState = CursorLockMode.None;

    }

    /// <summary>
    /// Metodo pra Renaudar el juego
    /// </summary>
    public void ResumeGame()
    {
        // cambia el estado del juego
        ChangeState(GameState.Game);

        // reanuda el juego
        Time.timeScale = 1f;

        // desactiva la pantalla de pausa
        pauseMenu.alpha = 0f;

        //ocultamos el cursor
        Cursor.visible = false;
        //bloqueamos el cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// metodo que comprueba si esta pausado el juego y lo renauda
    /// </summary>
    void CheckForPauseAndResume()
    {
        if (currentState == GameState.Paused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    /// <summary>
    /// Metodo para reiniciar la partida
    /// </summary>
    public void Restart()
    {
        //Si se reinicia la partida tras una pausa, hay que asegurar que el tiempo transcurrira con normalidad
        Time.timeScale = 1;

        //recuperamos el indice de la escena actual y la cargamos nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Metodo para ir al Menu Principal
    /// </summary>
    public void MainMenu(string sceneName)
    {
        ChangeSceneController.instance.ChangeScene(sceneName);
    }

}
