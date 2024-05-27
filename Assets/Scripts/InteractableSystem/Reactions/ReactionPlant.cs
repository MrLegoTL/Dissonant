using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReactionPlant : Reaction
{
    public GameObject plant;
    public MeshRenderer plantRenderer;
    public float moveTime = 3f;
    public GameObject lightPlants;
    public ParticleSystem particles;
    //referencia al audiosource que reproducira el sonido
    public AudioSource audioSource;  
    //sonido a reproducir
    public AudioClip audioClip;


    protected override void React()
    {
        plant.transform.DOMoveY(26f, moveTime);
        //asigno el clip de sonido
        audioSource.clip = audioClip;
        DestroyPlant();
        
    }

    public void DestroyPlant()
    {
        StartCoroutine(WaitToDestroyPlant());
    }

    private IEnumerator WaitToDestroyPlant()
    {
        while(plant.transform.position.y <= 25)
        {
            yield return new WaitForEndOfFrame();
        }

        plantRenderer.enabled = false;
        lightPlants.SetActive(true);
        particles.Play();
        //inicio la reproduccion del sonido
        audioSource.Play();



    }
    
}
