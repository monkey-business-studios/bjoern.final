using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{
    // global variables
    public Transform footSpawn;
    public Animator anim;

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

    // animation
    public bool inputBear_heavyAttack = false;

    void Awake()
    {
        lightAttackTrigger = transform.FindDeepChild("LightAttackTrigger").GetComponent<Collider>();
        heavyAttackTrigger = transform.FindDeepChild("HeavyAttackTrigger").GetComponent<Collider>();
        //closeByEnemy = GetClosestEnemy(GameObject.Find("AllEnemies").transform, transform.position);
        stoneSpawn = GameObject.Find("StoneSpawn").transform;
        footSpawn = GameObject.Find("Foot").transform;
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        // ---Input---
        // Heavy Attack
        if (Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.JoystickButton3))
        {
            if ((startGlobalCooldown == false) && (startHeavyAttackCooldown == false))
            {
                inputBear_heavyAttack = true;
                Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            }
        }

        // ---Cooldowns---
        // Global Cooldown
        if ((startGlobalCooldown == true) && (globalCooldownSet < globalColldownTimer))
        {
            globalCooldownSet += Time.deltaTime;
        }
        else
        {
            startGlobalCooldown = false;
            globalCooldownSet = 0f;
        }

        // Light Attack Cooldown
        if ((startLightAttackCooldown == true) && (lightAttackCooldownSet < lightAttackCooldownTime))
        {
            lightAttackCooldownSet += Time.deltaTime;
        }
        else
        {
            startLightAttackCooldown = false;
            lightAttackCooldownSet = 0f;
        }
        
        // Heavy Attack Cooldown
        if ((startHeavyAttackCooldown == true) && (heavyAttackCooldownSet < heavyAttackCooldownTime))
        {
            heavyAttackCooldownSet += Time.deltaTime;
        }
        else
        {
            startHeavyAttackCooldown = false;
            heavyAttackCooldownSet = 0f;
        }

        // Grab Stone Cooldown
        if ((startGrabCooldown == true) && (grabCooldownSet < grabCooldownTime))
        {
            grabCooldownSet += Time.deltaTime;
        }
        else
        {
            startGrabCooldown = false;
            grabCooldownSet = 0f;
        }

        // ---Animation---
        anim.SetBool("InputBear_HeavyAttack", inputBear_heavyAttack);
    }

    
    // ---Attacking Methods---
    // Light Attack
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

    // Heavy Attack
    void startHeavyAttack()
    {
        Instantiate(HeavyParticlePrefab, footSpawn.position, footSpawn.rotation);
        heavyAttackTrigger.enabled = true;
        startGlobalCooldown = true;
        startHeavyAttackCooldown = true;
        Debug.Log("heavy attack");
    }

    void endHeavyAttack()
    {
        heavyAttackTrigger.enabled = false;
        inputBear_heavyAttack = false;
    }

    // Grab & Throw
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

    /*
    // Cooldown
    float Cooldown(float timer)
    {
        float timerSet = 0f;
        timerSet += Time.deltaTime;
        Debug.Log(timerSet);

        if (timerSet >= timer)
        {
            return 0;
        }
        else
        {
            return timerSet;
        }
    }
    */
}