using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    // global
    private Rigidbody rb;

    //load Animator , CombatScript and HealthScript
    private Animator anim;                          // reference to the animator on the character+
	public CharacterCombat characterCombat;
    public CharacterHealth characterHealth;

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

    // sprint
    private bool canSprint = true;
    public float sprintDurationSet = 0f;
    public float sprintDurationTimer = 1.5f;
    private bool startSprintCooldown = false;
    public float sprintCooldownSet = 0f;
    public float sprintCooldownTimer = 5.0f;

	//animationen
    public string animstatus;
    private float inputH;
    private bool inputJumping = false;
    private bool inputBear_Attack = false;
    private bool inputBear_Run = false;
    private bool inputBear_Bite = false;
    private bool inputBear_WalkAttack = false;
    private bool inputBear_Dmg = false;
    private bool inputBear_Die = false;
    private bool inputBear_heavyAttack = false;

    private bool inputBear_ThrowStone = false;
    private bool inputBear_Grab = false;
    private bool stoneInHand = false;
    private bool oneGrab = true;
    public bool standangriff = false;


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
            inputJumping = true;
        }

        // sprint
        if ((canSprint) && (grounded) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton8)))
        {
            sprintDurationSet += Time.deltaTime;
            maxSpeed = 25;
            inputBear_Run = true;
            if (sprintDurationSet >= sprintDurationTimer)
            {
                canSprint = false;
                maxSpeed = 10;
                startSprintCooldown = true;
                sprintDurationSet = 0.0f;
                inputBear_Run = false;
            }

        }
        else
        {
            inputBear_Run = false;
            if (sprintDurationSet > 0)
            {
                sprintDurationSet -= Time.deltaTime;
                maxSpeed = 10;
            }
        }


        // Cooldowns
        if (startSprintCooldown)
        {
            sprintCooldownSet += Time.deltaTime;
            if (sprintCooldownSet >= sprintCooldownTimer )
            {
                startSprintCooldown = false;
                canSprint = true;
                sprintCooldownSet = 0.0f;
            }
        }
		
		//Code for Animations ----------------------------------------------------------------------------------
		   if (moveDirection != 0 /*&& characterCombat.stoneGrabbed == false*/)
        {
           

            inputBear_WalkAttack = Input.GetKeyDown(KeyCode.Mouse0);
            anim.SetBool("InputBear_WalkAttack", inputBear_WalkAttack);

        }
        else if (moveDirection != 0 /*&& characterCombat.stoneGrabbed == true*/)
        {
            inputBear_ThrowStone = Input.GetKeyDown(KeyCode.Mouse0);
            anim.SetBool("InputBear_Throw_Stone", inputBear_ThrowStone);
        }

        //Wenn er sich nicht bewegt wird die Steh Animation abgespielt und hat 2 veschiedene AngriffsAnimationen
        else if (moveDirection == 0)
        {
            standangriff = Input.GetKeyDown(KeyCode.Mouse0);

            if (characterCombat.stoneGrabbed == true && standangriff == true)

            {
                inputBear_ThrowStone = true;
                anim.SetBool("InputBear_Throw_Stone", inputBear_ThrowStone);
            }
            else if (characterCombat.stoneGrabbed == false && standangriff == true)
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

            if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
               
                
            }
        }

        if (characterCombat.stoneGrabbed == true && oneGrab == true)
        {
            inputBear_Grab = true;
            oneGrab = false;
        }

        else if (characterCombat.stoneGrabbed == false)
        {
            oneGrab = true;
            inputBear_Grab = false;
        }

        

        //Animation checking
        
       
        anim.SetBool("InputBear_Grab", inputBear_Grab);

        inputH = Input.GetAxis("Horizontal");
        
        anim.SetFloat("InputH", inputH);
        anim.SetBool("InputJumping", inputJumping);
        anim.SetBool("InputBear_Run", inputBear_Run);
        

        // zuweisung der moveDirection (-1 bis 1)
        moveDirection = Input.GetAxis("Horizontal");
      
			//------------------------------------------------------
    }


        
    void Flip()
    {
        facingRight = !facingRight;         // Setzt facingRight auf true oder false, je nachdem welche Bewegungstaste gedrückt wurde (a=false, d=true) (siehe moveDirection) Charakter muss mit dem Gesicht nach Rechts starten!
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

	void ResetInputBear_AttackEvent()
    {
        inputBear_Grab = false;
        inputBear_Attack = false;
        inputBear_Bite = false;
        anim.SetBool("InputBear_Grab", inputBear_Grab);
        anim.SetBool("InputBear_Attack", inputBear_Attack);
        anim.SetBool("InputBear_Bite", inputBear_Bite);
    }
    
    void SetThrowStoneFalse()
    {
        inputBear_ThrowStone = false;
        anim.SetBool("InputBear_Throw_Stone", inputBear_ThrowStone);
    }
    void jumping()
    {
        rb.AddRelativeForce(new Vector2(0, jumpSpeed));
    }
    void ResetInputJumpingFalse()
    {
        inputJumping = false;
    }
    void ResetInputHeavyAttackFalse()
    {
        inputBear_heavyAttack = false;
    }
}
