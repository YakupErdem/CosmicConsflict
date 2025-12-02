using System.Collections;
using UnityEngine;

namespace UI
{
public class TimeScaleSmoother : MonoBehaviour
{
    public static TimeScaleSmoother instance;

    private void Awake()
    {
        instance = this;
    }

    public void PauseSmooth(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothTo(0f, duration));
    }

    public void ResumeSmooth(float duration)
    {
        StopAllCoroutines();
        StartCoroutine(SmoothTo(1f, duration));
    }

    private IEnumerator SmoothTo(float target, float duration)
    {
        float start = Time.timeScale;
        float t = 0f;
        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / duration);
            Time.timeScale = Mathf.Lerp(start, target, p);
            yield return null;
        }
        Time.timeScale = target;
    }
}
}
