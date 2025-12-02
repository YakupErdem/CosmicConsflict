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
    public float questionSpawnDelay = 10f;
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
        StartCoroutine(QuestionSpawnLoop());
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

    public void AddPoint(int amount)
    {
        point += amount;
        pointText.text = point.ToString(CultureInfo.CurrentCulture);
    }

    public void EndGame()
    {
        Debug.Log("Game Ended");
        FindObjectOfType<SceneChanger>().LoadScene("Menu");
    }

    IEnumerator QuestionSpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(questionSpawnDelay);
            if (!isGameStarted) continue;
            if (QuestionManager.instance != null && QuestionBalloonSpawner.instance != null)
            {
                if (!QuestionManager.instance.HasRemainingQuestions()) yield break;
                QuestionBalloonSpawner.instance.Spawn();
            }
        }
    }
}
