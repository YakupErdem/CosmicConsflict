using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipAmmo : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += Vector3.up * (Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().GetHit();
            Instantiate(GameManager.instance.puffEffect).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
