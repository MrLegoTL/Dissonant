using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    //referencia al chareacter controller
    public CharacterController player;

    [Header("Movement")]
    //velocidad de desplazamiento
    public float speed = 2f;
    //Almacenamos de forma temporal el valor de los ejes horizontal y vertical
    private float moveHorizontal;
    private float moveVertical;
    //Vector3 que definira la direccion de desplazamiento
    private Vector3 movement = new Vector3();

    [Header("Player Rotation")]
    //variables donde alamcenamos de forma temporal el valor de la rotacion del player
    private float rotationHorizontal;
    private float rotationVertical = 0f;
    //velocidad de rotacion
    public float sensitivity = 2f;
    //referencia al tranform de la cabeza
    public Transform head;
    //permite inveretir la rotacion vertical de la camara
    public bool invertView = false;
    //variable con la que controlar la velocidad vertical del jugador
    private float verticalV;

    [Header("Jump")]
    //fuerza con la que realizara el salto
    public float jumpForce = 4f;

    [Header("Interact")]
    //Disctancia maxima para interactuar
    public float interactDistance = 2f;
    //Referencia al script de los interactuables
    private InteractableObjects currentInteractable;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //lo situamos el primero para asegurarnos de que disponemos de isGrounded para el resto del ciclo
        ApplyGravity();
        Controls();
        Rotation();
        Movement();
    }

    /// <summary>
    /// Gestionar los Inputs
    /// </summary>
    private void Controls()
    {
        //recuperamos el valor del eje horizontal
        moveHorizontal = Input.GetAxis("Horizontal");
        //recuperamos el valor del eje Vertical
        moveVertical = Input.GetAxis("Vertical");

        //recuperamos el desplazamiento del ratón para realizar la rotación del player
        rotationHorizontal = Input.GetAxis("Mouse X") * sensitivity;
        rotationVertical += Input.GetAxis("Mouse Y") * sensitivity * (invertView ? 1 : -1);
        //limitamos la rotacion para qu esolo pueda tener valores entre -90 y 90 grados
        rotationVertical = Mathf.Clamp(rotationVertical, -90f, 90f);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            InteractWithObject();
        }
    }

    /// <summary>
    /// Realiza las acciones de movimiento
    /// </summary>
    private void Movement()
    {

        movement.Set(moveHorizontal, 0, moveVertical);

        //Estamos orientando el vector de movimiento aplicandole la misma rotacion que el objeto player
        //esta oprecion se realiza multiplicando el Quaternion por un Vector3 (NO ES CONMUTATIVO)
        movement = transform.rotation * movement;
        //para evitar que nos podamos desplazar más rapido en diagonal, clampeamos el vector
        movement = Vector3.ClampMagnitude(movement, 1);
        //le indicamos al character controller, que realizaremos un desplazamiento, indicando el vector de direccion
        //multiplicado por la velocidad y por el delta time
        player.Move(movement * speed * Time.deltaTime);
    }

    /// <summary>
    /// Aplicamos la rotacion al jugador
    /// </summary>
    private void Rotation()
    {
        //rotamos de forma especifica una cantidad en el eje Y
        transform.Rotate(0, rotationHorizontal, 0);
        //rotamos verticalmente la cabeza del jugador
        head.localEulerAngles = new Vector3(rotationVertical, 0, 0);
    }

    /// <summary>
    /// Simula la gravedad, detecta grounded,etc
    /// </summary>
    private void ApplyGravity()
    {
        //aplicmaos la velocidad vertical actual
        player.Move(Vector3.up * verticalV * Time.deltaTime);

        //si el player esta tocando el suelo
        if (player.isGrounded)
        {
            //configuramos la velocidad, con la definida en el sistema de fisicas
            verticalV = Physics.gravity.y;
        }
        else
        {
            //si no se encuentra tocando el suelo, la velocidad vertical se irá decrementando con el tiempo
            verticalV += Physics.gravity.y * Time.deltaTime;
            //limitamos la velocidad de caida para que siempre se mantenga dentro de unos margenes de seguridad que impidan que se pueda romper el sistema de colisiones
            verticalV = Mathf.Clamp(verticalV, -50f, 50f);
        }
    }

    /// <summary>
    /// Realiza el salto cambiando la velocidad vertical
    /// </summary>
    private void Jump()
    {
        //cambiamos el valor de la velocidad vertical, para que se aplique la siguiente iteración
        if (player.isGrounded)
        {
            verticalV = jumpForce;
        }
    }

    private void InteractWithObject()
    {
        //Obterner la direccion en la que mira la camara
        Vector3 cameraDirection = Camera.main.transform.forward;

        RaycastHit hit;

        // Realizar un raycast desde la posición del jugador en la dirección de la cámara
        if (Physics.Raycast(transform.position,cameraDirection, out hit, interactDistance))
        {
            InteractableObjects objects = hit.collider.GetComponent<InteractableObjects>();
            if(objects != null && objects.CompareTag("Interactable"))
            {
                currentInteractable = objects;
                //Llama a metodo para interactua con el objeto
                currentInteractable.Interact();

            }
        }
    }
}
