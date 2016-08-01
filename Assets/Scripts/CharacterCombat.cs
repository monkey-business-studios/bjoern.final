using UnityEngine;
using System.Collections;

public class CharacterCombat : MonoBehaviour {

    public float clawSpeed = 600.0f;
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
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
	}

    void Attack()
    {
        clone = Instantiate(clawPrefab, clawSpawn.position, clawSpawn.rotation) as Rigidbody;
        clone.AddForce(clawSpawn.transform.right * clawSpeed);
    }
}
