using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeLanguageText : MonoBehaviour
{
    public TMP_Text text;
    
    public string spanishText;
    public string englishText;


    // Start is called before the first frame update
    void Start()
    {
       text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (TranslateManager.instance.defaultLanguage)
        {
            case "Spanish":
                text.text = spanishText; 
                break;
            case "English":
                text.text = englishText;
                break;

        } 
    }

    
}
