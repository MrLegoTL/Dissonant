using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

public class FPSController : MonoBehaviour
{
    //referencia al chareacter controller
    public CharacterController player;
    public Animator anim;

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
    public float interactDistance = 1f;
    //Referencia al script de los interactuables
    public InteractableObjects currentInteractable;
    //layers con los que será posible interactuar con la mirada
    public LayerMask interactableLayer;
    //para almacenar el resultado de impacto del raycast
    private RaycastHit hit;
    //el objeto actualmente en la mano del jugador
    
    public Transform handToObject;

    [Header("IndicatorDisplay")]
    //imagen que se mostrará cuando no haya elementos interactivos a la vista
    public Image spot;
    public Image hand;


    public static Action OnEnterPressed;

    //public static FPSController instance;

    //private void Awake()
    //{
    //    if (instance == null) instance = this;  
    //}

    // Start is called before the first frame update
    void Start()
    {
        SceneInitialPosition();
        ObjectInHand();
        if (DataManager.instance.staticPlayer)
        {
            player.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //lo situamos el primero para asegurarnos de que disponemos de isGrounded para el resto del ciclo
        ApplyGravity();
        Controls();
        Rotation();
        Movement();
        InteractWithObject();
        IndicatorDisplay();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnEnterPressed?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //intentamos recuperar el componente interactuable del objeto colisionado
        currentInteractable = other.GetComponent<InteractableObjects>();

        //si estamos en contacto con un interactuable, mostramos su nombre por consola
        if (currentInteractable != null)
        {
            Debug.Log("Interactable : " + other.name);
        }
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
        if(Input.GetKeyDown(KeyCode.E) && currentInteractable !=null)
        {
            currentInteractable.Interact();
            
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
        //Vector3 cameraDirection = Camera.main.transform.forward;



        //// Realizar un raycast desde la posición del jugador en la dirección de la cámara
        //if (Physics.Raycast(transform.position,cameraDirection, out hit, interactDistance))
        //{
        //    InteractableObjects objects = hit.collider.GetComponent<InteractableObjects>();
        ////    if(objects != null && objects.CompareTag("Interactable"))
        ////    {
        ////        currentInteractable = objects;
        ////        //Llama a metodo para interactua con el objeto
        ////        currentInteractable.Interact();

        ////    }
        ////    if (objects != null && objects.CompareTag("Clicked"))
        ////    {
        ////        currentInteractable = objects;
        ////        //Llama a metodo para interactua con el objeto
        ////        currentInteractable.Interact();
        ////        anim.SetTrigger("Click");

        ////    }
        ////    if(objects != null && objects.CompareTag("PickUp"))
        ////    {
        ////        currentInteractable = objects;
        ////        //Llama a metodo para interactua con el objeto
        ////        currentInteractable.Interact();

        ////        PickUpObject(objects.gameObject);
        ////    }
        //}

        //lanzamos un raycast desde la cabeza del jugador
        if (Physics.Raycast(head.position,
                           head.forward,
                           out hit,
                           interactDistance,
                           interactableLayer))
        {
            //si se produce un impacto
            //intentamos recuperar el componente interactable del objeto impactado
            // y si disponen de el, lo consideramos como el interactuable actual
            currentInteractable = hit.collider.GetComponent<InteractableObjects>();
        }
        else
        {
            //si no hya impactado asumimos que no hay interactables al alcance, asi que ponemos a null el  interacdtable actual
            currentInteractable = null;
        }
    }

    public void IndicatorDisplay()
    {
        hand.enabled = currentInteractable != null && !currentInteractable.triggerInteract;

        spot.enabled = (currentInteractable != null && currentInteractable.triggerInteract) || currentInteractable == null;
    }

    public void SceneInitialPosition()
    {
        GameObject playerPositon = GameObject.Find(DataManager.instance.playerPosition);
        if (playerPositon != null)
        {
            transform.position = playerPositon.transform.position;
            transform.rotation = playerPositon.transform.rotation;
        }
    }

    public void ObjectInHand()
    {
        if (DataManager.instance.CheckObjectInHand() == null) return;
        GameObject temp = Instantiate(DataManager.instance.CheckObjectInHand(), handToObject.position, DataManager.instance.CheckObjectInHand().transform.rotation);
        temp.transform.parent = handToObject;       
        
    }
  
}
