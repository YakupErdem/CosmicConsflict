using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuffAnim : MonoBehaviour
{
    [SerializeField] public float destroyTime = 1;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
