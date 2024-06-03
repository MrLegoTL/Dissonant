using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionScene : Reaction
{
    public string sceneName;
    public Transform playerPosition;

    protected override void React()
    {
        SceneController.instance.FadeAndLoadScene(sceneName);
        GameManager.instance.ChangeMusicScenes(sceneName);
        playerPosition = SceneController.instance.playerPosition;
    }
}
