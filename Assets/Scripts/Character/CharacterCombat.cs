using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour {

    public float clawSpeed = 2000.0f;
    public Transform clawSpawn; // Der Ort wo der "Schuss/Klaue" spawnt
    public Rigidbody clawPrefab;
    

    Rigidbody clone;

    void Awake()
    {
        clawSpawn = GameObject.Find("ClawSpawn").transform; // Der Variable wird das entsprechende GameObject zugewiesen
    }
	
	void Start ()
    {
	
	}
	

	void Update ()
    {
        // Attack MouseButton1
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LightAttack();
        }
	}

    void LightAttack()
    {
        clone = Instantiate(clawPrefab, clawSpawn.position, clawSpawn.rotation) as Rigidbody;
        clone.AddForce(clawSpawn.transform.right * clawSpeed);
    }

    void HeavyAttack()
    {
        clone = Instantiate(clawPrefab, clawSpawn.position, clawSpawn.rotation) as Rigidbody;
        clone.AddForce(clawSpawn.transform.right * clawSpeed);
    }
}
