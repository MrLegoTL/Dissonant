using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//para los campos de texto de TMP   
using TMPro;
//para usar el canvas group
using UnityEngine.UI;

public class TextCointainer : MonoBehaviour
{
    //referencia al canvas group del contenedor
    public CanvasGroup canvasGroup;
    //referencia la campo de texto
    public TMP_Text text;

    private void Start()
    {
        HideMessage();
    }

    private void OnEnable()
    {
        ReactionText.OnDisplayMessage += DisplayMessage;
        ReactionText.OnHideMessage += HideMessage;
    }

    private void OnDisable()
    {
        ReactionText.OnDisplayMessage -= DisplayMessage;
        ReactionText.OnHideMessage -= HideMessage;
    }

    /// <summary>
    /// Muestra el texto en pantalla
    /// </summary>
    /// <param name="message"></param>
    /// <param name="textColor"></param>
    public void DisplayMessage(string message, Color textColor)
    {
        //asignamos el texto del mensaje
        text.text = message;
        //cambiamos el color del texto
        text.color = textColor;
        //hacemos visible el panel del dialogo
        canvasGroup.alpha = 1;
        
    }

    /// <summary>
    /// Oculta el panel de dialogo
    /// </summary>
    public void HideMessage()
    {
        //vaciamos el texto mostrado
        text.text = "";
        //desactivamos el panel
        canvasGroup.alpha = 0;
    }
}
