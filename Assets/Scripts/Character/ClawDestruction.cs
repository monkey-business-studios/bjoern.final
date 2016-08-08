using UnityEngine;
using System.Collections;

public class ClawDestruction : MonoBehaviour {

    public float lifeSpan = 1.0f; //lifetime des Schuss/Klaue
	
	void Start ()
    {
        Destroy(gameObject, lifeSpan);
	}
	

}
