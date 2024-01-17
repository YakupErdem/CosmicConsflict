using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //
    [HideInInspector] public float point = 0;
    public GameObject puffEffect;
    public float speed = 1;
    public float speedIncrease, maxSpeed, delayReduce, minDelay;
    public bool isGameStarted;
    //
    [SerializeField] private Text pointText;
    private void Awake()
    { 
        instance = this;
    }

    private void Start()
    {
        Invoke(nameof(StartGame), 1.5f);
    }

    private void StartGame()
    {
        isGameStarted = true;
        StartCoroutine(AddPoint());
    }

    IEnumerator AddPoint()
    {
        yield return new WaitForSeconds(0.3f);
        point++;
        pointText.text = point.ToString(CultureInfo.CurrentCulture);
        if (point % 50 == 0)
        {
            Debug.Log("Speed increased");
            if(speed < maxSpeed) speed += speedIncrease;
            if(EnemySpawner.instance.delay > minDelay)EnemySpawner.instance.delay -= delayReduce;
        }
        if (point % 20 == 0)
        {
            AmmoSpawner.instance.SpawnAmmo();
        }
        //
        StartCoroutine(AddPoint());
    }

    public void EndGame()
    {
        Debug.Log("Game Ended");
        FindObjectOfType<SceneChanger>().LoadScene("Menu");
    }
}
