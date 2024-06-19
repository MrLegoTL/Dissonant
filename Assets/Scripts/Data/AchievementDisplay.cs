using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementDisplay : MonoBehaviour
{
    public string achievementName;
    public Image image;
    public TMP_Text name;
    public TMP_Text description;
    public GameObject shadow;
    public string textnameKey;
    public string textdescriptionKey;
    // Start is called before the first frame update
    void Start()
    {
        Achievement result = DataManager.instance.data.achievements.Where(a => a.name == achievementName).FirstOrDefault();

        image.sprite = Resources.Load<Sprite>("AchievementSprites/" + result.imageName);
        name.text = TranslateManager.instance.GetString(textnameKey);
        description.text = TranslateManager.instance.GetString(textdescriptionKey);
        //si esta desbloqueado dessctivamos la sombra
        shadow.SetActive(!result.unlocked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
