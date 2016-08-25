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
	
    void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        levelReload = GetComponent<LevelReload>();

        aura = transform.FindDeepChild("Bear_TOP").GetComponent<Renderer>();
        auraFur = transform.FindDeepChild("BEAR_FUR").GetComponent<Renderer>();
		
		anim = GetComponent<Animator>();
    }
	
	 void SetDeadToTrue()
    {
        inputBear_Dead = false;
        anim.SetBool("InputBear_Dead", inputBear_Dead);
    }
	
	
	void Update ()
    {
        
        if (charHealth <= 0)
        {
            //dead
            auraColor = new Color(.4f, .4f, .4f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
            auraFur.material.SetColor("_Color", auraColor);
            _characterMovement.enabled = false;
            levelReload.LevelReset();
			if (dieOnceBool == false)
            {
                dieOnceBool = true;
                Debug.Log("TODTODOTODOD");
                inputBear_Dead = true;
                anim.SetBool("InputBear_Dead", inputBear_Dead);
			}
        }
        else
        {
            //alive
            colorValue = (charHealth / 2000.0f) + 0.5f; // berechnung des farbwertes für die textur
            auraColor = new Color(colorValue, colorValue, colorValue, 1.0f);
            aura.material.SetColor("_Color", auraColor);
            auraFur.material.SetColor("_Color", auraColor);
        }

        //debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            charHealth = charHealth - 50;
            Debug.Log("hp-50");
        }

    }

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
            Debug.Log(charHealth);
        }
    }
}
