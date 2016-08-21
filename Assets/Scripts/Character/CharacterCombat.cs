using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{    
    // global
    private bool isAttacking = false;

    // light attack
    private Collider lightAttackTrigger;
    public float lightAttackTimer = 0.0f;
    public float lightAttackCooldown = 0.3f;
    public float lightAttackDamage = 25.0f;
    
    // heavy attack
    private Collider heavyAttackTrigger;
    public float heavyAttackTimer = 0.0f;
    public float heavyAttackCooldown = 1.0f;
    public float heavyAttackDamage = 75.0f;

    // grab
    public float grabTimer = 0.0f;
    public float grabCooldown = 1.0f;
    public bool stoneGrabbed = false;
    public float stoneDamage;
    private Transform grabDetecter;
    //private Transform closeByEnemy;
    float dist;
    public float throwSpeed = 2000.0f;
    public float throwSpeed2 = 1000.0f;
    private Transform stoneSpawn;
    public Rigidbody stonePrefab;
    Rigidbody cloneStone;

    void Awake()
    {
        lightAttackTrigger = transform.FindDeepChild("LightAttackTrigger").GetComponent<Collider>();
        heavyAttackTrigger = transform.FindDeepChild("HeavyAttackTrigger").GetComponent<Collider>();
        //closeByEnemy = GetClosestEnemy(GameObject.Find("AllEnemies").transform, transform.position);
        stoneSpawn = GameObject.Find("StoneSpawn").transform;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2)) && !isAttacking) //light
        {
            lightAttack();
        }
        else if ((Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton3)) && !isAttacking) //heavy
        {
            heavyAttack();
        }
        else if ((Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1)) && !isAttacking) //grab
        {
            grabStone();
        }
        else
        {
            isAttacking = false;
            lightAttackTrigger.enabled = false;
            heavyAttackTrigger.enabled = false;
        }
 
    }


    void lightAttack()
    {
        if(stoneGrabbed == false)
        {
            lightAttackTimer = lightAttackCooldown;
            lightAttackTrigger.enabled = true;
            Debug.Log("light attack");
        }
        else
        {
            throwStone();
        }

    }

    void heavyAttack()
    {
        isAttacking = true;
        heavyAttackTimer = heavyAttackCooldown;
        heavyAttackTrigger.enabled = true;
        Debug.Log("heavy attack");
    }

    void grabStone()
    {
        isAttacking = true;
        stoneGrabbed = true;


        //dist = Vector3.Distance(closeByEnemy.position, transform.FindChild("GrabDetecter").position);
        //Debug.Log(dist);
    }

    void throwStone()
    {

        cloneStone = Instantiate(stonePrefab, stoneSpawn.position, stoneSpawn.rotation) as Rigidbody;
        cloneStone.AddForce(stoneSpawn.transform.up * throwSpeed);
        cloneStone.AddForce(stoneSpawn.transform.right * throwSpeed2);
        stoneGrabbed = false;
    }

    // closest enemy script
    static Transform GetClosestEnemy(Transform enemies, Vector3 currentPosition)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        //Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }
}