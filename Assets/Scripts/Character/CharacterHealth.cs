using UnityEngine;
using System.Collections;

public class CharacterHealth : MonoBehaviour {

    public float charHealth = 1000.0f;
    private CharacterMovement characterMovement;
    private LevelReload levelReload;
    public Renderer aura;
    public Color auraColor;
    private float colorValue;

    void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>(); // Der Variable wird das Script CharakterMovement zugewiesen
        levelReload = GetComponent<LevelReload>();
        aura = transform.FindDeepChild("Bear_MOD").GetComponent<Renderer>();

    }
	
	void Update ()
    {
        
        if (charHealth <= 0)
        {
            //dead
            auraColor = new Color(.4f, .4f, .4f, 1.0f);
            aura.material.SetColor("_Color", auraColor);
            characterMovement.enabled = false;
            levelReload.LevelReset();
        }
        else
        {
            //alive
            colorValue = (charHealth / 2000.0f) + 0.5f; // berechnung des farbwertes für die textur
            auraColor = new Color(colorValue, colorValue, colorValue, 1.0f);
            aura.material.SetColor("_Color", auraColor);
        }

        //debug
        if (Input.GetKeyDown(KeyCode.K))
        {
            charHealth = charHealth - 50;
            Debug.Log("hp-50");
        }

    }


}
