using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{
    // global variables
    public Transform footSpawn;

    // global cooldown
    public bool globalCooldown;
    public float globalCooldownSet = 0f;
    public float globalColldownTimer = 0.2f;
    

    // light attack
    private Collider lightAttackTrigger;
    public bool lightAttackCooldown;
    public float lightAttackTimer = 0.0f;
    public float lightAttackCooldownTime = 3.0f;
    public float lightAttackDamage = 25.0f;
    
    // heavy attack
    private Collider heavyAttackTrigger;
    public bool heavyAttackCooldown;
    public float heavyAttackTimer = 0.0f;
    public float heavyAttackCooldownTime = 1.0f;
    public float heavyAttackDamage = 75.0f;
    public GameObject HeavyParticlePrefab;
    GameObject heavyParticle;

    // grab
    public bool grabCooldown;
    public float grabTimer = 0.0f;
    public float grabCooldownTime = 5.0f;
    public bool stoneGrabbed = false;
    public float stoneDamage;
    private Transform grabDetecter;
    //private Transform closeByEnemy;
    float dist;
    public float throwSpeedUp = 2000.0f;
    public float throwSpeedRight = 1000.0f;
    private Transform stoneSpawn;
    public Rigidbody stonePrefab;
    Rigidbody cloneStone;

    void Awake()
    {
        lightAttackTrigger = transform.FindDeepChild("LightAttackTrigger").GetComponent<Collider>();
        heavyAttackTrigger = transform.FindDeepChild("HeavyAttackTrigger").GetComponent<Collider>();
        //closeByEnemy = GetClosestEnemy(GameObject.Find("AllEnemies").transform, transform.position);
        stoneSpawn = GameObject.Find("StoneSpawn").transform;
        footSpawn = GameObject.Find("Foot").transform;

        lightAttackCooldown = false;
        heavyAttackCooldown = false;
        grabCooldown = false;
        globalCooldown = false;
    }


    void FixedUpdate()
    {
        if (globalCooldown == false)
        {
            //light
            if (lightAttackCooldown == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    if (stoneGrabbed == false)
                    {
                        lightAttackTrigger.enabled = true;
                        globalCooldown = true;
                        lightAttackCooldown = true;
                        Debug.Log("light attack");
                    }
                    else
                    {
                        throwStone();
                    }

                }
            }
            else
            {
                lightAttackTrigger.enabled = false;
            }

            // heavy
            if (heavyAttackCooldown == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    Instantiate(HeavyParticlePrefab, footSpawn.position, footSpawn.rotation);
                    heavyAttackTrigger.enabled = true;
                    globalCooldown = true;
                    heavyAttackCooldown = true;
                    Debug.Log("heavy attack");
                }
            }
            else
            {
                heavyAttackTrigger.enabled = false;
            }

            // grab stone
            if (grabCooldown == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    stoneGrabbed = true;
                    globalCooldown = true;
                    grabCooldown = true;
                    Debug.Log("grab stone");
                }

            }
        }


        // CooldownTimer

        // global cooldown
        if ((globalCooldown == true) && (globalCooldownSet < globalColldownTimer))
        {
            globalCooldownSet += Time.deltaTime;
        }
        else
        {
            globalCooldown = false;
            globalCooldownSet = 0f;
        }

        // light
        if ((lightAttackCooldown == true) && (lightAttackTimer < lightAttackCooldownTime))
        {
            lightAttackTimer += Time.deltaTime;
        }
        else
        {
            lightAttackCooldown = false;
            lightAttackTimer = 0f;
        }

        //heavy
        if ((heavyAttackCooldown == true) && (heavyAttackTimer < heavyAttackCooldownTime))
        {
            heavyAttackTimer += Time.deltaTime;
        }
        else
        {
            heavyAttackCooldown = false;
            heavyAttackTimer = 0f;
        }

        //grab
        if ((grabCooldown == true) && (grabTimer < grabCooldownTime))
        {
            grabTimer += Time.deltaTime;
        }
        else
        {
            grabCooldown = false;
            grabTimer = 0f;
        }
    }
    


    /*void grabStone()
    {

        dist = Vector3.Distance(closeByEnemy.position, transform.FindChild("GrabDetecter").position);
        Debug.Log(dist);
    }*/

    void throwStone()
    {
        cloneStone = Instantiate(stonePrefab, stoneSpawn.position, stoneSpawn.rotation) as Rigidbody;
        cloneStone.AddForce((stoneSpawn.transform.up * throwSpeedUp) + (stoneSpawn.transform.right * throwSpeedRight));
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