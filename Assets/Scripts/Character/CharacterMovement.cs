using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    public float maxSpeed = 10.0f;
    public float speedInAir = 5.0f;
    public bool facingRight = true;
    public float moveDirection; // Bekommt seinen Wert, je nachdem welche Bewegungstaste gedrückt wird. ( a = kleiner 0 bis -1    ;   d = größer 0 bis 1 )
    public bool doubleJump = false;
    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    public float whatIsSpeed;

    public Rigidbody rb;
    
    // Awake Funktion wird direkt, wenn das Spiel startet, aufgerufen.
    void Awake()
    {
        groundCheck = GameObject.Find("GroundCheck").transform;
    }



    void Start ()
    {
        rb = GetComponent<Rigidbody>(); // In most cases you should not modify the velocity directly, as this can result in unrealistic behaviour. https://docs.unity3d.com/ScriptReference/Rigidbody-velocity.html

    }



    void FixedUpdate ()
    {
        
        // Den Radius des groundCheck definieren
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        //
        

        // flipping/facing
        if (moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if(moveDirection < 0.0f && facingRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            moveDirection = .0f;
        }

        // doublejump and aircontrol
        if (grounded)
        {
            doubleJump = false;
            rb.velocity = new Vector2(moveDirection * maxSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveDirection * speedInAir, rb.velocity.y);
        }
        whatIsSpeed = rb.velocity.magnitude; //anzeigen der geschwindigkeit des character(rigidbody)



    }
	
	

	void Update ()
    {
        // zuweisung der werte für moveDirection (a or d)(1 or -1)
        moveDirection = Input.GetAxis("Horizontal");

        // jumping, space
        if ((grounded || !doubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddRelativeForce(new Vector2(0, jumpSpeed));

            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
        }

    }



    void Flip()
    {
        facingRight = !facingRight;         // Setzt facingRight auf true oder false, je nachdem welche Bewegungstaste gedrückt wurde (a=false, d=true) (siehe moveDirection) Charakter muss mit dem Gesicht nach Rechts starten!

        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }





}
