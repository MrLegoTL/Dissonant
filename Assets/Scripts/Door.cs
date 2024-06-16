using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour
{
    public Animator animator;
    public bool doorOpened = false;

    [Header("DetectDron")]
    public float detectionRadius = 5f;
    public float raycastMaxDistance = 2f;
    public float offsetDistance = 1f;
    public LayerMask detectionLayer;

    private void Start()
    {
        // Obtener el componente Animator del objeto padre
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("No Animator component found on the parent object. Please add an Animator component.");
        }
    }
    private void Update()
    {
        DetectDron();
    }

    void DetectDron()
    {
        // Posición del objeto desde donde se lanza el SphereCast
        Vector3 origin = transform.position + transform.forward * offsetDistance;

        // Realiza el SphereCast
        RaycastHit[] hits = Physics.SphereCastAll(origin, detectionRadius, transform.forward, raycastMaxDistance, detectionLayer);

        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("Dron") && !doorOpened)
            {
                animator.SetTrigger("Open");
                doorOpened = true;
                return;
            }

        }

        
    }
    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + transform.forward * offsetDistance;
        // Dibujar el SphereCast en la escena para visualización
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(origin, detectionRadius);
        Gizmos.DrawLine(origin, origin + transform.forward * raycastMaxDistance);
    }

}
