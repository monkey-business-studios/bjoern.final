using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int enemyHealth = 100;

    void Start ()
    {
	
	}


    void Update()
    {
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

}
