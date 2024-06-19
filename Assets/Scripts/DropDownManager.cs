using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DropDownManager : MonoBehaviour
{
    public string spanish = "Spanish";
    public string english = "English";
    TMP_Dropdown dropdown;

    // Start is called before the first frame update
    public void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        TranslateData();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TranslateData()
    {
        if(TranslateManager.instance.defaultLanguage == spanish)
        {
            dropdown.value = 0;
        }
        if(TranslateManager.instance.defaultLanguage == english)
        {
            dropdown.value = 1;
        }
    }
    public void ChangeLanguages(int value)
    {
        if (value == 0)
        {
            Language(spanish);
        }
        if (value == 1)
        {
            Language(english);
        }
    }
    public void Language(string language)
    {
        TranslateManager.instance.OnLanguageChange(language);
    }
}
