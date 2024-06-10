using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionScene : Reaction
{
    public string sceneName;
    public string positionInScene;
    

    protected override void React()
    {
        DataManager.instance.playerPosition = positionInScene;
        SceneController.instance.FadeAndLoadScene(sceneName);
        GameManager.instance.ChangeMusicScenes(sceneName);
       
        
    }
}
