using UnityEngine;
using System.Collections;

public class EnemySpear : MonoBehaviour
{
    public Collider spearTrigger;
    public float spearDamage = 100.0f;

    void Awake()
    {
        spearTrigger = transform.FindDeepChild("SpearTrigger").GetComponent<Collider>();
    }
}
