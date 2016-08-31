using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public float bossDamage = 100.0f;
    public float bossHealth = 200.0f;
    public CharacterCombat _CharacterCombat;



    void Update()
    {

        if (bossHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }


    // ---Trigger/Collider Events---

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightAttackTrigger"))
        {
            bossHealth -= _CharacterCombat.lightAttackDamage;
            Debug.Log(bossHealth);
        }

        if (other.CompareTag("HeavyAttackTrigger"))
        {
            bossHealth -= _CharacterCombat.heavyAttackDamage;
            Debug.Log(bossHealth);
        }

        if (other.CompareTag("StoneProjectileTrigger"))
        {
            bossHealth -= _CharacterCombat.stoneDamage;
            Debug.Log(bossHealth);
        }

    }
}
