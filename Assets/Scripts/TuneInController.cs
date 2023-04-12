using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuneInController : MonoBehaviour
{
    bool isSpawned = false;
    SphereSpawner sphereSpawner;

    private void Awake()
    {
        sphereSpawner = GetComponent<SphereSpawner>();
    }
    // Start is called before the first frame update
    public void spawnObject()
    {
        if (!isSpawned)
        {
            isSpawned = true;
            sphereSpawner.Spawn();
        }
        else
        {
            isSpawned=false;
            sphereSpawner.RemoveSource();
        }

    }
}
