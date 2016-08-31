using UnityEngine;
using System.Collections;

public class EnemyMacheteHealth : MonoBehaviour
{
    public float enemyHealth = 100.0f;
    public CharacterCombat _CharacterCombat;
    private Rigidbody rb;
    public Animator anim;
    public bool enemyGetHit = false;
    public bool enemyDead = false;
    public bool enemyDeadOnce = false;

    public EnemyMachete _EnemyMachete;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        _EnemyMachete = GetComponent<EnemyMachete>();
    }

    void Update()
    {
        anim.SetBool("EnemyGetHit", enemyGetHit);
        anim.SetBool("EnemyDead", enemyDead);

        if (enemyGetHit == true)
        {
            enemyGetHit = false;
        }

        if (enemyHealth <= 0 && enemyDeadOnce == false)
        {
            enemyDead = true;
            enemyDeadOnce = true;
            Invoke("DestroyEnemy", 3);

            _EnemyMachete.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LightAttackTrigger"))
        {
            enemyGetHit = true;
            enemyHealth -= _CharacterCombat.lightAttackDamage;
            Debug.Log(enemyHealth);
        }

        if (other.CompareTag("HeavyAttackTrigger"))
        {
            enemyGetHit = true;
            enemyHealth -= _CharacterCombat.heavyAttackDamage;
            Debug.Log(enemyHealth);
        }

        if (other.CompareTag("StoneProjectileTrigger"))
        {
            enemyGetHit = true;
            enemyHealth -= _CharacterCombat.stoneDamage;
            rb.AddExplosionForce(10f, transform.position, 5f);
            Debug.Log("HIT");
        }
        else
        {
            //enemyGetHit = false;
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

    void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
    void EnemyDeadOnce()
    {
        enemyDead = false;
    }
}
