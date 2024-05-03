using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    [SerializeField] Transform[] zombieSpawnLocations;
    [SerializeField] GameObject zombie;

    void SpawnZombie()
    {
        int randomPos = Random.Range(0, zombieSpawnLocations.Length);
        Debug.Log( "zombie spawned" );
        Instantiate( zombie, zombieSpawnLocations[randomPos] );
    }
}
