using UnityEngine;

public class QuestionBalloon : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        if (!GameManager.instance.isGameStarted)
        {
            return;
        }
        transform.position += Vector3.down * (Time.deltaTime * speed * GameManager.instance.speed);
    }

    public void GetHit()
    {
        if (QuestionManager.instance != null)
        {
            QuestionManager.instance.TryOpenQuestion();
        }
        Destroy(gameObject);
    }
}

