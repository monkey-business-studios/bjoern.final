using UnityEngine;
using System.Collections;

public class EnemyMachete : MonoBehaviour
{
    private Transform GetCharacter;
    public Animator anim;
    public Rigidbody  eBody;

    public float MoveSpeed = 4.0f;
    public float MaxDist = 10.0f;
    public float MinDist = 3.5f;
    public float attackdistance = 3.5f;
    float viewingdistance = 16.0f;

    bool transformvar; 
    bool playerinsight;

    private string animstatus;
    
    private Vector3 targetposition;

    public Collider macheteTrigger;    // der collider der waffe
    public float macheteDamage = 75.0f;

    void Awake()
    {
        GetCharacter = GameObject.FindGameObjectWithTag("Character").transform;
        macheteTrigger = transform.FindDeepChild("MacheteTrigger").GetComponent<Collider>(); // zuweisung des colliders
    }

    //-------------------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        anim = GetComponent<Animator>();
        playerinsight = false;
        transformvar = false;
        eBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;

    }
    
    //-------------------------------------------------------------------------------------------------------------------------------
    // 3 verschiedene Arten wie sich der Gegner verhält wenn der Spieler nicht zu sehen ist
    public void turningaround()
    {
        transformvar = false;
        transform.RotateAround(transform.position, transform.up, 90f);
        
    }

    public void walkingaround()
    {
        // walk in a specific area
    }

    public void sleeping()
    {
        // do nothing

    }
    public void walking()
    {

        Vector3 targetposition = new Vector3(GetCharacter.position.x, this.transform.position.y, GetCharacter.position.z);

        this.transform.LookAt(targetposition);

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //Debug.Log("walking function");
        if (animstatus == "Standing")
        {
            macheteTrigger.enabled = false;
            anim.Play("en_1_arm|En1_stand 0", -1, 1);
            animstatus = "Walking";
        }

        if (animstatus == "Attacking")
        {
            macheteTrigger.enabled = false;
            anim.Play("en_1_arm|En1_atk_1 0 0", -1, 2f);
            animstatus = "Walking";
        }
    }  
        public void attacking()
    {

        if (animstatus == "Walking")
        {
            macheteTrigger.enabled = true;
            anim.Play("en_1_arm|En1_walk_1 0 0", -1, 1);
            animstatus = "Attacking";
        }
    }
    
        //-------------------------------------------------------------------------------------------------------------------------------
        void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, Player.position));

        // Wenn der Spieler NICHT in Sicht ist wird in einem BESTIMMTETn Interval "turningaround" ausgeführt
        if (transformvar == false && playerinsight == false)
        {

            transformvar = true;
            Invoke("turningaround", 2);
            anim.Play("en_1_arm|En1_stand");
            animstatus = "Standing";

        }

        // Wenn der Spieler in der Nähe ist guckt der Gegner ihn an und hört auf sich umzudrehen, wenn nicht dreht er sich weiter
        if (Vector3.Distance(transform.position, GetCharacter.position) <= viewingdistance)
        {

            playerinsight = true;
            Vector3 targetposition = new Vector3(GetCharacter.position.x, this.transform.position.y, GetCharacter.position.z);

            this.transform.LookAt(targetposition);
        }

        else
        {
            playerinsight = false;

        }


  
        // Wenn der Spieler nah genug am Gegner drann ist fängt er an ihn zu verfolgen
        if (Vector3.Distance(transform.position, GetCharacter.position) <= MaxDist && Vector3.Distance(transform.position, GetCharacter.position) >= MinDist)
        {
            Invoke("walking", 0.25f);
           
        }
    
        if (Vector3.Distance(transform.position, GetCharacter.position) <= MaxDist)
        {
            //Here Call any function U want Like Shoot at here or something
        }
        else
        {
          
        }
         
        
        // Wenn der Gegner nah genug am Spieler drann ist soll er angreifen
        if (Vector3.Distance(transform.position, GetCharacter.position) <= attackdistance)
        {
            Invoke("attacking", 0.25f);
        }
    }
}