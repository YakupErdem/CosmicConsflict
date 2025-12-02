using System.Globalization;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager instance;
    [SerializeField] private int pointsPerRocket = 10;

    private void Awake()
    {
        instance = this;
    }

    public void AddRocketScore()
    {
        if (!GameManager.instance.isGameStarted) return;
        GameManager.instance.AddPoint(pointsPerRocket);
    }
}
