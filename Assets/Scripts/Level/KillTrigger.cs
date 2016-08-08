using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour
{
    private CharacterHealth characterHealth;
    //private EnemyHealth enemyHealth;

    void Awake ()
    {
        characterHealth = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterHealth>();
        //enemyHealth
    }
	
	


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Character")
        {
            Debug.Log("PENG PENG PENG");
            characterHealth.charHealth = 0;
        }
    }
}
