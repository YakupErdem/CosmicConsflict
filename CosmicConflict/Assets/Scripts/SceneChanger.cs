using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static bool SceneChanged;
    //
    [SerializeField] private GameObject sceneLoader;
    private String _newSceneName;

    private void Start()
    {
        Application.targetFrameRate = 144;
        //
        if (SceneChanged)
        {
            SceneChanged = false;
            sceneLoader.SetActive(true);
            sceneLoader.GetComponent<Animator>().SetTrigger("Open");
        }
    }

    public void LoadScene(string sceneName)
    {
        sceneLoader.SetActive(true);
        sceneLoader.GetComponent<Animator>().SetTrigger("Close");
        SceneChanged = true;
        //
        _newSceneName = sceneName;
        //
        Invoke(nameof(LoadNewScene), 1);
    }
    
    private void LoadNewScene() => SceneManager.LoadScene(_newSceneName);
}
