using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;
public class CarMovementPhysics : MonoBehaviour
{
    [Header("Statistics")]
    
    [Tooltip("The max speed the car can reach")]
    public float speed = 15;
    
    [Tooltip("Represent how much the car accelerate. The greater the value, the faster the car will reach max speed")]
    public float accelerationSpeed = 15;
    
    [Tooltip("Represent how much the car deccelerate. The greater the value, the faster the car will become stationary")]
    public float decelerateSpeed = 15;
    
    [Tooltip("The speed at wich the car will turn. Come on, you don't need an explanation !")]
    public float turnSpeed;
    
    [Tooltip("The speed at wich the linear velocity of the car line up with the car orientaion (think of it as the sliperyness of the car)")]
    public float driftCatchSpeed = 2.25f;
    
    

    [Header("Advanced Statistics")]
    
    [Tooltip("The maximum angle between the direction of the velocity and the car direction. Think of it as how much the car can go sideway whil drifting")]
    public float bufferTiming;
    
    [Tooltip("The maximum angle between the direction of the velocity and the car direction. Think of it as how much the car can go sideway whil drifting")]
    public float maxDriftAngle = 65;



    [Header("References")]
   

    [SerializeField] private GameObject ChocVFX;

    public CarInput carInput;


    private float maxSpeed;

    public float speedLost;
    private Vector3 moveDirection;
    private float buffer;
    private float TurnInput; //the value of the input used for turning [-1,1]
    public float isAccelerating;
    private Rigidbody rb;
    public float speedActu = 0;
    private GameObject e;
    private MultiplayerManager multiplayer;
    
   

    //______________________________________________[Subscribing Function To read Values Of Inputs Events]____________________________________________


    public void SetTurnInput(InputAction.CallbackContext ctx)
    {
        TurnInput = ctx.ReadValue<float>();
    }

    public void SetIsAccelerating(InputAction.CallbackContext ctx)
    {
        isAccelerating = ctx.ReadValue<float>();
    }
   

    //__________________________________________________________[Algorithm]__________________________________________________________________________
    

    private void Awake()
    {
       

    }

    void Start()
    {
        moveDirection = transform.forward;
        rb = GetComponent<Rigidbody>();
        maxSpeed = speed;
    }

  



    void Update()    
    { 
        rb.linearVelocity = moveDirection * speedActu;

        Turning(TurnInput);
        
        moveDirection = Vector3.RotateTowards(moveDirection, transform.forward, driftCatchSpeed * Time.deltaTime, 0.0f);

        if (Vector3.Angle(transform.forward, moveDirection) >= maxDriftAngle)
        {
            moveDirection = Quaternion.AngleAxis(maxDriftAngle * -TurnInput, Vector3.up) * transform.forward;
            
        }

        ActualiseSpeed();

        buffer -= Time.deltaTime;//changer la methode de calcul du bufferTime de la collision
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (buffer <= 0)
        {
            speedActu -= speedActu / 100 * speedLost;
            rb.AddForce(collision.contacts[0].normal * 10);
          
            GameObject choc = Instantiate(ChocVFX, collision.contacts[0].point, Quaternion.identity);
            choc.transform.LookAt(choc.transform.position + collision.contacts[0].normal);
        }
        buffer = bufferTiming;
    }
    //________________________________________________________________________[Function Declaration]__________________________________________________________________________

    public void Turning(float TurnAmount)
    {
        transform.Rotate(new Vector3(0, TurnAmount * turnSpeed * Time.deltaTime, 0));
    }

    void ActualiseSpeed()//Accelerate until reaching max speed
    {
        if (isAccelerating >= 1)
        {
            speedActu += accelerationSpeed * Time.deltaTime;
            speedActu = Mathf.Clamp(speedActu, 0, maxSpeed);
        }
        else
        {
            speedActu -= decelerateSpeed * Time.deltaTime;
            speedActu = Mathf.Clamp(speedActu, 0, maxSpeed);
        }
    }


}
