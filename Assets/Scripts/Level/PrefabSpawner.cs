using UnityEngine;
using System.Collections;

public class PrefabSpawner : MonoBehaviour
{
    public Transform[] spawnLocations;
    public GameObject[] whatToSpawnPrefab;
    public GameObject[] whatToSpawnClone;

    void Start()
    {
        SpawnPrefab();
    }

    void SpawnPrefab()
    {
        whatToSpawnClone[0] = Instantiate(whatToSpawnPrefab[0], spawnLocations[0].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        whatToSpawnClone[1] = Instantiate(whatToSpawnPrefab[1], spawnLocations[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
    }

}
