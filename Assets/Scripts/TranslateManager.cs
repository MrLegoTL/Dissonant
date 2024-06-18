using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PARA PODER LEER EL XML
using System.Xml;
using UnityEngine.UI;
using TMPro;

public class TranslateManager : MonoBehaviour
{
    //Idioma por defecto a utilizar si no exite el idioma local
    public string defaultLanguage = "Spanish";
    //listado de items tipo array, que permite indices alfanumericos
    public Dictionary<string, string> strings;

    public static TranslateManager instance;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //indicamos que el gameobject no sera destruido entre escenas
            DontDestroyOnLoad(gameObject);
        }

        //si se trata de una instancia distinta a la cual
        else if (instance != this)
        {
            //la destruimos
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //recuperamos el lenguaje del sistema operativo
        //string language = Application.systemLanguage.ToString();

        // Cargar el idioma predeterminado al inicio
        LoadLanguage(Application.systemLanguage.ToString());

    }

    public void LoadLanguage(string language)
    {
        //intentamos recuperar el archivo xml con el idioma del sistema
        TextAsset textAsset = (TextAsset)Resources.Load(language, typeof(TextAsset));

        //si no existe el archivo
        if (textAsset == null)
        {
            //RECUPERMAOS EL IDIOMA POR DEFECTO
            textAsset = (TextAsset)Resources.Load(defaultLanguage, typeof(TextAsset));
        }

        //creamos una variable de tipo XMLDocument para gestionar el xml
        XmlDocument xml = new XmlDocument();
        //cargamos el xml utilizando el textAsset
        xml.LoadXml(textAsset.text);
        //llamamos al metido que carga los literales en el diccionario a partir del xml
        SetLanguage(xml);
    }
   

    /// <summary>
    /// Carga todas las strings contenidas en el xml dentro  del  diccionario
    /// </summary>
    /// <param name="xml"></param>
    public void SetLanguage(XmlDocument xml)
    {
        //inicializamos el diccionario 
        strings = new Dictionary<string, string>();
        //recuperamos el bloque de xml que contoiene los literales de texto
        XmlElement element = xml.DocumentElement["lang"];

        //mediante este metodo recuperamos un tipo enumerador, qu enos permitirar recorrer el xml como si fuera un foreach
        IEnumerator elemEnum = element.GetEnumerator();

        //mientras move next devuelva true, significa qu ehay elementos por recorrer
        while (elemEnum.MoveNext())
        {
            //recuperamos el elemento actual
            XmlElement xmlItem = (XmlElement)elemEnum.Current;
            //añadimos al diccionario, el literal de texto usando el name como indice
            strings.Add(xmlItem.GetAttribute("name"), xmlItem.InnerText);
        }

    }

    /// <summary>
    /// Recupera un literal de texto del diccionario mediante el indicerecibido como parametro
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public string GetString(string name)
    {
        //si no existe la cadena solicitada
        if (!strings.ContainsKey(name))
        {
            //avisamos al desarrollador
            Debug.LogWarning("La cadena no existe: " + name);
            //devolvemos una cadena vacía
            return "";
        }

        //si llegamos hast aqui tenemos la seguridad de que existe elemento en el diccionario
        return strings[name];
    }

    /// <summary>
    /// Método que cambia el idioma basado en la opción seleccionada en el menú desplegable.
    /// </summary>
    /// <param name="dropdown"></param>
    public void OnLanguageChange(TMP_Dropdown dropdown)
    {
        string selectedLanguage = dropdown.options[dropdown.value].text;
        LoadLanguage(selectedLanguage);
        defaultLanguage = selectedLanguage;
        Debug.Log(selectedLanguage);

    }
}
