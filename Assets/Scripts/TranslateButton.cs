using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Xml;

[CreateAssetMenu(menuName = "Languages/TranslateButton")]
public class TranslateButton : ScriptableObject
{
    public string spanish = "Spanish";
    public string english = "English";

    
    public void ChangeLanguages(int value)
    {
       if(value == 0)
        {
            Language(spanish);
        }
       if(value == 1)
        {
            Language(english);
        }
    }

    public void Language(string language)
    {
        TranslateManager.instance.OnLanguageChange(language);
        //TranslateManager.instance.defaultLanguage = language;

        //language = Application.systemLanguage.ToString();
        ////intentamos recuperar el archivo xml con el idioma del sistema
        //TextAsset textAsset = (TextAsset)Resources.Load(TranslateManager.instance.defaultLanguage, typeof(TextAsset));

        ////si no existe el archivo
        //if (textAsset == null)
        //{
        //    //RECUPERMAOS EL IDIOMA POR DEFECTO
        //    textAsset = (TextAsset)Resources.Load(TranslateManager.instance.defaultLanguage, typeof(TextAsset));
        //}

        ////creamos una variable de tipo XMLDocument para gestionar el xml
        //XmlDocument xml = new XmlDocument();
        ////cargamos el xml utilizando el textAsset
        //xml.LoadXml(textAsset.text);
        ////llamamos al metido que carga los literales en el diccionario a partir del xml
        //TranslateManager.instance.SetLanguage(xml);
    }
}
