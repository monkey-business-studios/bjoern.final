using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

    //movement
    public float charHealth = 1000.0f;
    private CharacterMovement _characterMovement;

    //damage taken from enemy
    public EnemyMachete _enemyMachete;
    public EnemySpear _enemySpear;

    //reload lvl
    private LevelReload levelReload;
	
	//load animator
	private Animator anim;

    //aura
    public Renderer aura;
    public Renderer auraFur;
    public Color auraColor;
    private float colorValue;

	private bool inputBear_Dead = false;
    private bool dieOnceBool = false;

    private Rigidbody rb;

    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        levelReload = GetComponent<LevelReload>();
        rb = GetComponent<Rigidbody>();

        anim = GetComponent<Animator>();
    }
	

	
	
	void Update ()
    {
        
        if (charHealth <= 0)
        {
            CharacterIsDead();
        }
        else
        {
            CharacterIsAlive();
        }

        //debug killswitch
        if (Input.GetKeyDown(KeyCode.K))
        {
            charHealth = charHealth - 50;
            Debug.Log("hp-50");
        }
    }

    // ---Character Status---

    void CharacterIsDead()
    {
        auraColor = new Color(.4f, .4f, .4f, 1.0f);
        aura.material.SetColor("_Color", auraColor);
        auraFur.material.SetColor("_Color", auraColor);
        _characterMovement.enabled = false;
        levelReload.LevelReset();
        // franz animationen
        inputBear_Dead = false;
        anim.SetBool("InputBear_Dead", inputBear_Dead);
        
        if (dieOnceBool == false)
        {
            dieOnceBool = true;
            inputBear_Dead = true;
            anim.SetBool("InputBear_Dead", inputBear_Dead);
        }
    }

    void CharacterIsAlive()
    {
        colorValue = (charHealth / 2000.0f) + 0.5f; // berechnung des farbwertes für die textur
        auraColor = new Color(colorValue, colorValue, colorValue, 1.0f);
        aura.material.SetColor("_Color", auraColor);
        auraFur.material.SetColor("_Color", auraColor);
    }

    // ---Trigger/Collider Events---
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MacheteTrigger"))
        {
            charHealth -= _enemyMachete.macheteDamage;
            Debug.Log(charHealth);
        }
        else if (other.CompareTag("SpearTrigger"))
        {
            charHealth -= _enemySpear.spearDamage;
            rb.AddForce(transform.up * 5000f);
            rb.AddForce(-(transform.right) * 100000f);
        }
    }

    // franz animationen
    void SetDeadToTrue()
    {
        inputBear_Dead = false;
        anim.SetBool("InputBear_Dead", inputBear_Dead);
    }
}
