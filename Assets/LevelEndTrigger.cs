using UnityEngine;
using System.Collections;

public class LevelEndTrigger : MonoBehaviour
{
    public CharacterCombat _CharacterCombat;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            _CharacterCombat.throwSpeedUp = 15000f;
        }
    }

}
