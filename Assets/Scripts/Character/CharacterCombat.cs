using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{
    // global variables
    public Transform footSpawn;

    // global cooldown
    public bool startGlobalCooldown = false;
    public float globalCooldownSet = 0f;
    public float globalColldownTimer = 0.2f;
    

    // light attack
    private Collider lightAttackTrigger;
    public bool startLightAttackCooldown = false;
    public float lightAttackCooldownSet = 0.0f;
    public float lightAttackCooldownTime = 3.0f;
    public float lightAttackDamage = 25.0f;
    
    // heavy attack
    private Collider heavyAttackTrigger;
    public bool startHeavyAttackCooldown = false;
    public float heavyAttackCooldownSet = 0.0f;
    public float heavyAttackCooldownTime = 1.0f;
    public float heavyAttackDamage = 75.0f;
    public GameObject HeavyParticlePrefab;

    // grab
    public bool startGrabCooldown = false;
    public float grabCooldownSet = 0.0f;
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

    }


    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2))
        {
            startLightAttack();
        }
        else
        {
            endLightAttack();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            startHeavyAttack();
        }
        else
        {
            endHeavyAttack();
        }

        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            grabStone();
        }

/*----------------------------------------backup-----------------------------------------------------------
        if (startGlobalCooldown == false)
        {
            //light
            if (startLightAttackCooldown == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.JoystickButton2))
                {
                    if (stoneGrabbed == false)
                    {
                        lightAttackTrigger.enabled = true;
                        startGlobalCooldown = true;
                        startLightAttackCooldown = true;
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
            if (startHeavyAttackCooldown == false)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton3))
                {
                    Instantiate(HeavyParticlePrefab, footSpawn.position, footSpawn.rotation);
                    heavyAttackTrigger.enabled = true;
                    startGlobalCooldown = true;
                    startHeavyAttackCooldown = true;
                    Debug.Log("heavy attack");
                }
            }
            else
            {
                heavyAttackTrigger.enabled = false;
            }

            // grab stone
            if (startGrabCooldown == false)
            {
                if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    stoneGrabbed = true;
                    startGlobalCooldown = true;
                    startGrabCooldown = true;
                    Debug.Log("grab stone");
                }

            }
        }
----------------------------------------backup-----------------------------------------------------------*/


        // Cooldowns

        // global cooldown
        if ((startGlobalCooldown == true) && (globalCooldownSet < globalColldownTimer))
        {
            globalCooldownSet += Time.deltaTime;
        }
        else
        {
            startGlobalCooldown = false;
            globalCooldownSet = 0f;
        }

        // light
        if ((startLightAttackCooldown == true) && (lightAttackCooldownSet < lightAttackCooldownTime))
        {
            lightAttackCooldownSet += Time.deltaTime;
        }
        else
        {
            startLightAttackCooldown = false;
            lightAttackCooldownSet = 0f;
        }

        //heavy
        if ((startHeavyAttackCooldown == true) && (heavyAttackCooldownSet < heavyAttackCooldownTime))
        {
            heavyAttackCooldownSet += Time.deltaTime;
        }
        else
        {
            startHeavyAttackCooldown = false;
            heavyAttackCooldownSet = 0f;
        }

        //grab
        if ((startGrabCooldown == true) && (grabCooldownSet < grabCooldownTime))
        {
            grabCooldownSet += Time.deltaTime;
        }
        else
        {
            startGrabCooldown = false;
            grabCooldownSet = 0f;
        }
    }

    // functions for attacking

    // light attack
    void startLightAttack()
    {
        if ((startGlobalCooldown == false) && (startLightAttackCooldown == false))
        {
            if (stoneGrabbed == false)
            {
                lightAttackTrigger.enabled = true;
                startGlobalCooldown = true;
                startLightAttackCooldown = true;
                Debug.Log("light attack");
            }
            else
            {
                throwStone();
            }
        }
    }

    void endLightAttack()
    {
        lightAttackTrigger.enabled = false;
    }

    // heavy attack
    void startHeavyAttack()
    {
        if ((startGlobalCooldown == false) && (startHeavyAttackCooldown == false))
        {
            Instantiate(HeavyParticlePrefab, footSpawn.position, footSpawn.rotation);
            heavyAttackTrigger.enabled = true;
            startGlobalCooldown = true;
            startHeavyAttackCooldown = true;
            Debug.Log("heavy attack");
        }
    }

    void endHeavyAttack()
    {
        heavyAttackTrigger.enabled = false;
    }

    // grab & throw stone
    void grabStone()
    {
        if ((startGlobalCooldown == false) && (startGrabCooldown == false))
        {
            stoneGrabbed = true;
            startGlobalCooldown = true;
            startGrabCooldown = true;
            Debug.Log("grab stone");
        }
    }
    void throwStone()
    {
        cloneStone = Instantiate(stonePrefab, stoneSpawn.position, stoneSpawn.rotation) as Rigidbody;
        cloneStone.AddForce((stoneSpawn.transform.up * throwSpeedUp) + (stoneSpawn.transform.right * throwSpeedRight));
        stoneGrabbed = false;
    }

    // nearest enemy
    //dist = Vector3.Distance(closeByEnemy.position, transform.FindChild("GrabDetecter").position);
    //Debug.Log(dist);






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