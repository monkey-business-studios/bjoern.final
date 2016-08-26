using UnityEngine;
using System.Collections;

public class EnemySpear : MonoBehaviour
{
    public Collider spearTrigger;
    public float spearDamage = 100.0f;

    public Transform GetCharacter;
    //public Animator anim;
    public Rigidbody eBody;

    public float MoveSpeed = 4.0f;
    public float MaxDist = 10.0f;
    public float MinDist = 3.5f;
    public float attackdistance = 3.5f;
    float viewingdistance = 16.0f;

    bool transformvar;
    bool playerinsight;

    private string animstatus;

    private Vector3 targetposition;

    void Start()
    {
        //anim = GetComponent<Animator>();
        playerinsight = false;
        transformvar = false;
        eBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;

    }

    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, Player.position));

        // Wenn der Spieler NICHT in Sicht ist wird in einem BESTIMMTETn Interval "turningaround" ausgeführt
        if (transformvar == false && playerinsight == false)
        {

            transformvar = true;


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

        }
    }


    public void walking()
    {

        Vector3 targetposition = new Vector3(GetCharacter.position.x, this.transform.position.y, GetCharacter.position.z);

        this.transform.LookAt(targetposition);

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

    }
}
