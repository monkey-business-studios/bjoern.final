using UnityEngine;
using System.Collections;

public class Destruction : MonoBehaviour {

    public float lifeSpan = 1.0f; //lifetime
	
	void Start ()
    {
        Destroy(gameObject, lifeSpan);
	}
	

}
