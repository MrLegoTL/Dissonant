using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public GameObject targetObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            ToggleObject();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Platform"))
        {
            ToggleObject();
        }
    }
    public void ToggleObject()
    {
        if (targetObject != null )
        {
            // Cambiar el estado activo del objeto
            bool isActive = targetObject.activeSelf;
            targetObject.SetActive(!isActive);


        }

    }
}
