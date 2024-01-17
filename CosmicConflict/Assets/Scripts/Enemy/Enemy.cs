using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    
    private void Start()
    {
        Destroy(gameObject, 15);
    }

    public void GetHit()
    {
        transform.position += new Vector3(0, .1f, 0);
        //
        if (health > 0)
        {
            health--;
        }
        else
        {
            Debug.Log(gameObject.name + " health: " + health);
            Debug.Log("Enemy " + gameObject.name + " dead");
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!GameManager.instance.isGameStarted)
        {
            return;
        }
        transform.position += Vector3.down * (Time.deltaTime * (speed * GameManager.instance.speed));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.EndGame();
            Instantiate(GameManager.instance.puffEffect).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}

public interface IDamageable
{
    public void GetHit();
}