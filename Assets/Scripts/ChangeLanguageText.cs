using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeLanguageText : MonoBehaviour
{
    public TMP_Text text;

    public string textKey;
    
    //public string spanishText;
    //public string englishText;

    private void OnEnable()
    {
        TranslateManager.onChangeLanguage += ChangeText;
    }

    private void OnDisable()
    {
        TranslateManager.onChangeLanguage -= ChangeText;
    }

    // Start is called before the first frame update
    void Start()
    {
       text = GetComponent<TMP_Text>();
       ChangeText();
    }

    // Update is called once per frame
    void Update()
    {
        //switch (TranslateManager.instance.defaultLanguage)
        //{
        //    case "Spanish":
        //        text.text = spanishText; 
        //        break;
        //    case "English":
        //        text.text = englishText;
        //        break;

        //}


        
    }

    public void ChangeText()
    {
        
        text.text = TranslateManager.instance.GetString(textKey);
    }

    
}
