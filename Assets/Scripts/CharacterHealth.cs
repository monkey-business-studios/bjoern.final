using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

    public int health = 1000;
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
        if (health == 1000)
        {
            auraColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 1000 && health > 900)
        {
            auraColor = new Color(.95f, .95f, .95f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 900 && health > 800)
        {
            auraColor = new Color(.90f, .90f, .90f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 800 && health > 700)
        {
            auraColor = new Color(.85f, .85f, .85f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 700 && health > 600)
        {
            auraColor = new Color(.80f, .80f, .80f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 600 && health > 500)
        {
            auraColor = new Color(.75f, .75f, .75f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 500 && health > 400)
        {
            auraColor = new Color(.70f, .70f, .70f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 400 && health > 300)
        {
            auraColor = new Color(.65f, .65f, .65f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 300 && health > 200)
        {
            auraColor = new Color(.60f, .60f, .60f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 200 && health > 100)
        {
            auraColor = new Color(.55f, .55f, .55f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        if (health <= 100 && health > 0)
        {
            auraColor = new Color(.5f, .5f, .5f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        // dead
        if (health <= 0)
        {
            characterMovement.enabled = false;
            levelReload.LevelReset();

        }

    }


}
