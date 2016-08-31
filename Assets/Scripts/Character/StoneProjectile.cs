using UnityEngine;
using System.Collections;

public class StoneProjectile : MonoBehaviour {

    public float lifeSpan = 5f; //lifetime
    private int rotateSpeed;
    private Vector3 spin;


    void Awake()
    {
        //transform.FindChild("stone").GetComponent<Renderer>().sortingLayerName = "Moveground";
    }

    void Start()
    {
        Destroy(gameObject, lifeSpan);
        rotateSpeed = Random.Range(200, 300);
        spin = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        //Debug.Log("spin vector = " + spin);
        //Debug.Log("rotate speed = " + rotateSpeed);
    }

    void FixedUpdate()
    {
        transform.Rotate(spin, rotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        lifeSpan = 0.2f;
        Destroy(gameObject, lifeSpan);
    }
    
}
