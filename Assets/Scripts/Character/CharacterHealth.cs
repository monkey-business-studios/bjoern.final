using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

    public int charHealth = 1000;
    private CharacterMovement characterMovement;
    private LevelReload levelReload;
    public Renderer aura;
    public Color auraColor;

    void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>(); // Der Variable wird das Script CharakterMovement zugewiesen
        levelReload = GetComponent<LevelReload>();
        aura = transform.FindDeepChild("Body").GetComponent<Renderer>();

    }
	
	void Update ()
    {
        if (charHealth == 1000)
        {
            auraColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 1000 && charHealth > 900)
        {
            auraColor = new Color(.95f, .95f, .95f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 900 && charHealth > 800)
        {
            auraColor = new Color(.90f, .90f, .90f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 800 && charHealth > 700)
        {
            auraColor = new Color(.85f, .85f, .85f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 700 && charHealth > 600)
        {
            auraColor = new Color(.80f, .80f, .80f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 600 && charHealth > 500)
        {
            auraColor = new Color(.75f, .75f, .75f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 500 && charHealth > 400)
        {
            auraColor = new Color(.70f, .70f, .70f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 400 && charHealth > 300)
        {
            auraColor = new Color(.65f, .65f, .65f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 300 && charHealth > 200)
        {
            auraColor = new Color(.60f, .60f, .60f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 200 && charHealth > 100)
        {
            auraColor = new Color(.55f, .55f, .55f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (charHealth <= 100 && charHealth > 0)
        {
            auraColor = new Color(.5f, .5f, .5f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        // dead
        if (charHealth <= 0)
        {
            auraColor = new Color(.4f, .4f, .4f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
            characterMovement.enabled = false;
            levelReload.LevelReset();

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            charHealth = charHealth - 100;
            Debug.Log("hp-100");
        }

    }


}
