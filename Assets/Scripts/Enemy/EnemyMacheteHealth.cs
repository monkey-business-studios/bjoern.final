using UnityEngine;
using System.Collections;

public class EnemyMacheteHealth : MonoBehaviour
{
    public float enemyHealth = 100.0f;
    public CharacterCombat _CharacterCombat;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightAttackTrigger"))
        {
            enemyHealth -= _CharacterCombat.lightAttackDamage;
        }

        if (other.CompareTag("HeavyAttackTrigger"))
        {
            enemyHealth -= _CharacterCombat.heavyAttackDamage;
        }

        if (other.CompareTag("StoneProjectileTrigger"))
        {
            enemyHealth -= _CharacterCombat.stoneDamage;
            rb.AddExplosionForce(10f, transform.position, 5f);
        }

    }


    void OnTriggerStay(Collider other)
    {
        // Wie lange er im gegner drinne ist
    }
    void OnTriggerExit(Collider other)
    {
        // wenn er rausgeht, Knock-back?
    }


}
