using UnityEngine;
using System.Collections;

public class GrenadeThrow : MonoBehaviour
{
    public float granateSpeed = 1500.0f;
    public float minGranateSpeed = 250.0f;
    private float speedRnd;
    public Transform granadeSpawn; // Der Ort wo der "Schuss" spawnt
    public Rigidbody granatePrefab;
    private Animator anim;
    public bool gThrow = true;
    public float timer;
    public float waitTime = 5.0f;
    public bool rotationRnd;
    public bool Boss_Attack = false;

    Rigidbody clone;
   void Awake ()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {


        if (timer == 0)
        {
            Boss_Attack = true;
            anim.SetBool("Boss_Attack", Boss_Attack);
        }
        else
        {
            Boss_Attack = false;
            anim.SetBool("Boss_Attack", Boss_Attack);
        }



        timer += Time.deltaTime;
        if (timer >= waitTime)
        {
            timer = 0;
        }
    }

    void ThrowGrenade()
    {
        speedRnd = Random.Range(minGranateSpeed, granateSpeed);
        clone = Instantiate(granatePrefab, granadeSpawn.position, granadeSpawn.rotation) as Rigidbody;
        clone.AddForce((-(transform.right) + (transform.up)) * speedRnd);
    }
}

