using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AmmoSpawner : MonoBehaviour
{
    public static AmmoSpawner instance;
    //
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject ammo;
    [SerializeField] private float minX, maxX;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnAmmo()
    {
        var spawnedAmmo = Instantiate(ammo, parent);
        spawnedAmmo.transform.position = new Vector3(Random.Range(minX, maxX), spawnedAmmo.transform.position.y, 0);
        Destroy(spawnedAmmo, 10);
    }
}
