using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    public float maxSpeed = 6.0f;
    public bool facingRight = true;
    public float moveDirection; // Bekommt seinen Wert, je nachdem welche Bewegungstaste gedrückt wird. ( a = kleiner 0 bis -1    ;   d = größer 0 bis 1 )
    public bool doubleJump = false;
    public float jumpSpeed = 600.0f;
    public bool grounded = false;
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

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
        // doublejump
        if (grounded)
        {
            doubleJump = false;
        }


        // Den Radius des groundCheck definieren und für welche LayerMask er gültig ist.
        grounded = Physics.CheckSphere(groundCheck.position, groundRadius, whatIsGround);

        // speed of the rigidbody/character
        rb.velocity = new Vector2(moveDirection * maxSpeed, rb.velocity.y);

        // facing
        if(moveDirection > 0.0f && !facingRight)
        {
            Flip();
        }
        else if(moveDirection < 0.0f && facingRight)
        {
            Flip();
        }


	}
	
	

	void Update ()
    {
        // move, wasd
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
