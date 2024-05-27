using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionSound : Reaction
{
    //referencia al audiosource que reproducira el sonido
    public AudioSource audioSource;
    //para especificar si el sonido se debera reproducir en bucle
    public bool loop;
    //sonido a reproducir
    public AudioClip audioClip;

    protected override void React()
    {
        //asigno el clip de sonido
        audioSource.clip = audioClip;
        //especifico si se reproducira en bucle
        audioSource.loop = loop;
        //inicio la reproduccion del sonido
        audioSource.Play();
    }
}
