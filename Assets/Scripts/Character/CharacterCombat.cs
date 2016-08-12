using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{

    //public float clawSpeed = 2000.0f;
    //public Transform clawSpawn; // Der Ort wo der "Schuss/Klaue" spawnt
    //public Rigidbody clawPrefab;
    //Rigidbody clone;

    private Collider lightAttackTrigger;
    private Collider heavyAttackTrigger;

    private bool isAttacking = false;

    public float lightAttackTimer = 0.0f;
    public float lightAttackCooldown = 0.3f;
    public float lightAttackDamage = 25.0f;

    public float heavyAttackTimer = 0.0f;
    public float heavyAttackCooldown = 1.0f;
    public float heavyAttackDamage = 75.0f;

    public float grabTimer = 0.0f;
    public float grabCooldown = 1.0f;
    public float grabDamage = 0.0f;
    public bool enemyCanBeGrabbed = false;
    
    void Awake()
    {
        //clawSpawn = GameObject.Find("ClawSpawn").transform; // Der Variable wird das entsprechende GameObject zugewiesen
        lightAttackTrigger = transform.FindDeepChild("LightAttackTrigger").GetComponent<Collider>();
        heavyAttackTrigger = transform.FindDeepChild("HeavyAttackTrigger").GetComponent<Collider>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking) //light
        {
            lightAttack();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking) //heavy
        {
            heavyAttack();
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl) && !isAttacking) //grab
        {
            //grabEnemy();
        }
        else
        {
            isAttacking = false;
            lightAttackTrigger.enabled = false;
            heavyAttackTrigger.enabled = false;
        }
 

        /* claw shooter
            void LightAttack()
            {
                clone = Instantiate(clawPrefab, clawSpawn.position, clawSpawn.rotation) as Rigidbody;
                clone.AddForce(clawSpawn.transform.right * clawSpeed);
            }
        */

    }


    void lightAttack()
    {
        isAttacking = true;
        lightAttackTimer = lightAttackCooldown;
        lightAttackTrigger.enabled = true;
        Debug.Log("light attack");
    }

    void heavyAttack()
    {
        isAttacking = true;
        heavyAttackTimer = heavyAttackCooldown;
        heavyAttackTrigger.enabled = true;
        Debug.Log("heavy attack");
    }



}
