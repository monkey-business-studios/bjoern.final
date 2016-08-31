using UnityEngine;
using System.Collections;

public class GrenadeDestroy : MonoBehaviour {

    public float lifeSpan = 1.0f; //lifetime des Schuss
    public GameObject particle;

    void Start()
    {
        Destroy(gameObject, lifeSpan);
    }
    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(particle, pos, rot);
        Destroy(gameObject);
    }
}
