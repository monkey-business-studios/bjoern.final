using UnityEngine;
using System.Collections;

public class EnemyMacheteHealth : MonoBehaviour
{
    public float enemyHealth = 100.0f;
    private float lightDamage;
    private float heavyDamage;

    void Awake()
    {
        lightDamage = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterCombat>().lightAttackDamage;
        heavyDamage = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterCombat>().heavyAttackDamage;
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
            enemyHealth -= lightDamage;
            Debug.Log(enemyHealth);
        }

        if (other.CompareTag("HeavyAttackTrigger"))
        {
            enemyHealth -= heavyDamage;
            Debug.Log(enemyHealth);
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
