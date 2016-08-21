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
    private float moveDirection;                // Bekommt seinen Wert, je nachdem welche Taste gedrückt wird. ( a = kleiner 0 bis -1    ;   d = größer 0 bis 1 )
    private float whatIsSpeed;                  // debug

    //jumping
    public float speedInAir = 5.0f;
    //public bool doubleJump = false;             // double jump check
    public float jumpSpeed = 600.0f;

    // groundcheck
    private bool grounded = false;
    private Transform groundCheck;
    private float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    //animationen
    public string animstatus;
    private float inputH;
    private bool inputJumping;
    private bool inputBear_Attack;
    private bool inputBear_Run;
    private bool inputBear_Bite;
    private bool inputBear_WalkAttack;
    private bool inputBear_Dmg;
    private bool inputBear_Die;

    public bool standangriff;

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

    void ResetInputBear_AttackEvent()
    {
        inputBear_Attack = false;
        inputBear_Bite = false;
        anim.SetBool("InputBear_Attack", inputBear_Attack);
        anim.SetBool("InputBear_Bite", inputBear_Bite);
        //Debug.Log("werte zurückgesetzt!");
    }

    void Update()
    {
        if (moveDirection != 0)
        {

            //Debug.Log(moveDirection);

            inputBear_WalkAttack = Input.GetKeyDown(KeyCode.Mouse0);
            anim.SetBool("InputBear_WalkAttack", inputBear_WalkAttack);

        }
        //Wenn er sich nicht bewegt wird die Steh Animation abgespielt und hat 2 veschiedene AngriffsAnimationen
        else if (moveDirection == 0)
        {
            standangriff = Input.GetKeyDown(KeyCode.Mouse0);

            if (standangriff == true)
            {
                int n = Random.Range(0, 2);
                if (n == 0)
                {
                    inputBear_Attack = Input.GetKeyDown(KeyCode.Mouse0);
                    anim.SetBool("InputBear_Attack", inputBear_Attack);

                }
                else if (n == 1)
                {
                    inputBear_Bite = Input.GetKeyDown(KeyCode.Mouse0);
                    anim.SetBool("InputBear_Bite", inputBear_Bite);

                }
                //Debug.Log(n);
                inputBear_Attack = standangriff;
                inputBear_Bite = standangriff;
            }
        }

        //Animation checking

        inputH = Input.GetAxis("Horizontal");
        inputJumping = Input.GetKeyDown(KeyCode.Space);
        inputBear_Run = Input.GetKey(KeyCode.LeftShift);

        anim.SetFloat("InputH", inputH);
        anim.SetBool("InputJumping", inputJumping);
        anim.SetBool("InputBear_Run", inputBear_Run);



        moveDirection = Input.GetAxis("Horizontal"); // moveDirection = -1 bis 1



        // jumping, space
        if ((grounded /*|| !doubleJump*/) && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0)))
        {
            rb.AddRelativeForce(new Vector2(0, jumpSpeed));

            /*
            if (!doubleJump && !grounded)
            {
                doubleJump = true;
            }
            */
        }
    }


        
    void Flip()
    {
        facingRight = !facingRight;         // Setzt facingRight auf true oder false, je nachdem welche Bewegungstaste gedrückt wurde (a=false, d=true) (siehe moveDirection) Charakter muss mit dem Gesicht nach Rechts starten!
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }


}
