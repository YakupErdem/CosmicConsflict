using UnityEngine;
using Random = UnityEngine.Random;

public class QuestionBalloonSpawner : MonoBehaviour
{
    public static QuestionBalloonSpawner instance;

    [SerializeField] private Transform parent;
    [SerializeField] private GameObject questionBalloonPrefab;
    [SerializeField] private float minX, maxX;

    private void Awake()
    {
        instance = this;
    }

    public void Spawn()
    {
        if (QuestionManager.instance == null) return;
        if (!QuestionManager.instance.HasRemainingQuestions()) return;
        var go = Instantiate(questionBalloonPrefab, parent);
        go.transform.position = new Vector3(Random.Range(minX, maxX), go.transform.position.y, 0f);
        Destroy(go, 15);
    }
}

