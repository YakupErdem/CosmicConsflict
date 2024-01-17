using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float amount = 12;

    private void Update()
    {
        if (!GameManager.instance.isGameStarted)
        {
            return;
        }
        transform.position += Vector3.down * (Time.deltaTime * speed * GameManager.instance.speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpaceShipShoot.instance.TakeAmmo(amount);
            //
            Instantiate(GameManager.instance.puffEffect).transform.position = transform.position;
            //
            Destroy(gameObject);
        }
    }
}
