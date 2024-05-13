using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//PARA PODER SERIALIZAR EN BINARIO
using System.Runtime.Serialization.Formatters.Binary;
//para poder leer/escribir en ficheros
using System.IO;
//PARA PODER FILTRAR LOS ARRAYS
using System.Linq;

public class DataManager : MonoBehaviour
{
    //Objeto que contendra toda la informacion de condiciones, items, etc.. asin como su estado
    public Data data;

    public string fileName = "data.dat";
    //combinacion de ruta + nombre de archivo
    private string dataPath;
    //singleton
    public static DataManager instance;

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

        //conformamos el dataPath combinando el persitente datapath con el nombre de archivo
        dataPath = Application.persistentDataPath + "/" + fileName;
        //para poder localizar m�s facilmente la carpeta de data
        Debug.Log(Application.persistentDataPath);

        //cargamos el data previo si este existe
        Load();
    }

    /// <summary>
    /// Guardar la informacion de data, de forma persistente
    /// </summary>
    [ContextMenu("Save")]
    public void Save()
    {
        dataPath = Application.persistentDataPath + "/" + fileName;
        //objeto utilizado para serializar / deserializar
        BinaryFormatter bf = new BinaryFormatter();
        // crea /  sobreescribe el fichero con los datos en binario
        FileStream file = File.Create(dataPath);
        //serializamos el contenido de nuetsro objetod de datos volcado al archivo
        bf.Serialize(file, data);
        // cerramos el stream una vez termiando el proceso
        file.Close();
    }

    /// <summary>
    /// Recupera la informacion almacenada en el disco
    /// </summary>
    [ContextMenu("Load")]
    public void Load()
    {
        dataPath = Application.persistentDataPath + "/" + fileName;
        //si no existe el archivo no hacemos nada
        if (!File.Exists(dataPath)) return;

        //objeto para serializar / deserializar
        BinaryFormatter bf = new BinaryFormatter();
        //apertura del fichero para su lectura
        FileStream file = File.Open(dataPath, FileMode.Open);
        //Deserializamos el fichero utilizando la estructura de la clase data
        data = (Data)bf.Deserialize(file);
        //una vez terminado cerramos el archivo
        file.Close();

    }

    /// <summary>
    /// Borra el fichero de guardado
    /// </summary>
    [ContextMenu("DelateData")]
    public void DeleteSaveFile()
    {
        dataPath = Application.persistentDataPath + "/" + fileName;
        try
        {
            //borra fisicamente el archivo
            File.Delete(dataPath);
        }
        catch (System.Exception)
        {
            Debug.Log("No existe el archivo");
        }

    }

    /// <summary>
    /// Cambia el estado de una condicion, al estado indicado
    /// </summary>
    /// <param name="conditionName"></param>
    /// <param name="done"></param>
    public void SetCondition(string conditionName, bool done)
    {
        //buscamos una condicion, que coincida con el nombre indicado
        Condition result = data.allCondition.Where(c => c.name == conditionName).FirstOrDefault();

        //si escite la condicion
        if (result != null)
        {
            //modificamos us estado asignando el valor indicado
            result.done = done;
        }
        else
        {
            Debug.LogWarning(conditionName + " La condicon no exite y no se puede modificar");
        }

    }

    /// <summary>
    /// Devuelve el estado en el que se encuentra la condicion recibida como parametro
    /// </summary>
    /// <param name="conditionName"></param>
    /// <returns></returns>
    public bool CheckCondition(string conditionName)
    {
        //buscamos la condicion en la lista
        Condition result = data.allCondition.Where(c => c.name == conditionName).FirstOrDefault();

        //si la condicion existe
        if (result != null)
        {
            //devuelvo su estado
            return result.done;
        }
        else
        {
            Debug.LogWarning(conditionName + " La condicion no existe y no se puede revisar");
            //si no existe la condicion, devolvemos un flase
            return false;
        }
    }

}
