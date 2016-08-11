using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour
{

    //public float clawSpeed = 2000.0f;
    //public Transform clawSpawn; // Der Ort wo der "Schuss/Klaue" spawnt
    //public Rigidbody clawPrefab;
    private Collider lightAttackTrigger;
    private Collider heavyAttackTrigger;
    private bool lightAttack = false;
    private float lightAttackTimer = 0.0f;
    private float lightAttackCooldown = 0.3f;
    public float lightAttackDamage = 25.0f;


    Rigidbody clone;

    void Awake()
    {
        //clawSpawn = GameObject.Find("ClawSpawn").transform; // Der Variable wird das entsprechende GameObject zugewiesen
        lightAttackTrigger = transform.FindDeepChild("LightAttackTrigger").GetComponent<Collider>();
        heavyAttackTrigger = transform.FindDeepChild("HeavyAttackTrigger").GetComponent<Collider>();
    }


    void Update()
    {
        // Attack MouseButton1
        if (Input.GetKeyDown(KeyCode.Mouse0) && !lightAttack)
        {
            lightAttack = true;
            lightAttackTimer = lightAttackCooldown;
            lightAttackTrigger.enabled = true;
            Debug.Log("light attack");
        }
        else
        {
            lightAttack = false;
            lightAttackTrigger.enabled = false;
        }


        /* claw shooter
            void LightAttack()
            {
                clone = Instantiate(clawPrefab, clawSpawn.position, clawSpawn.rotation) as Rigidbody;
                clone.AddForce(clawSpawn.transform.right * clawSpeed);
            }
        */

    }
}
