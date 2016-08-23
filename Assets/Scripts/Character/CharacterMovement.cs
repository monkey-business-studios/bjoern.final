using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    // global
    private Rigidbody rb;

    //animation
    private Animator anim;                          // reference to the animator on the character

    // walking
    public float maxSpeed = 10.0f;
    private bool facingRight = true;
    private float moveDirection;                    // Bekommt seinen Wert, je nachdem welche Taste gedrückt wird. ( a = kleiner 0 bis -1    ;   d = größer 0 bis 1 )
    private float whatIsSpeed;                      // debug

    //jumping
    public float speedInAir = 5.0f;
    //public bool doubleJump = false;               // double jump check
    public float jumpSpeed = 600.0f;

    // groundcheck
    private bool grounded = false;
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;


    void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
        transform.FindDeepChild("BEAR_FUR").GetComponent<Renderer>().sortingLayerName = "Moveground";
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }


    void FixedUpdate ()
    {
        // Den Radius des groundCheck definieren
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        // flipping/facing
        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if(moveDirection < 0.0f && facingRight)
        {
            Flip();
        }


        // doublejump and aircontrol
        if (grounded)
        {
            //doubleJump = false;
            rb.velocity = new Vector2(moveDirection * maxSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection * speedInAir, rb.velocity.y);
        }
        whatIsSpeed = rb.velocity.magnitude; //anzeigen der geschwindigkeit des rigidbody (debug)
        
    }


    void Update()
    {
        // zuweisung der moveDirection (-1 bis 1)
        moveDirection = Input.GetAxis("Horizontal");
        

        // jumping, space
        if ((grounded) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            rb.AddRelativeForce(new Vector2(0, jumpSpeed));
        }

        // sprint
        if ((grounded) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton8)))
        {
            maxSpeed = 25;
        }
        else
        {
            maxSpeed = 10;
        }

    }


        
    void Flip()
    {
        facingRight = !facingRight;         // Setzt facingRight auf true oder false, je nachdem welche Bewegungstaste gedrückt wurde (a=false, d=true) (siehe moveDirection) Charakter muss mit dem Gesicht nach Rechts starten!
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }


}
