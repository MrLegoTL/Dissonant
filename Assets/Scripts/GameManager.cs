using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    //configuramos la clase como singlenton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }

   
}
