using System.Collections;
using UnityEngine;

namespace UI
{
public class PopupAnimator : MonoBehaviour
{
    [SerializeField] private float inDuration1 = 0.12f;
    [SerializeField] private float inDuration2 = 0.18f;
    [SerializeField] private float outDuration = 0.12f;
    [SerializeField] private float startFactor = 0.5f;   // base/2
    [SerializeField] private float peakFactor = 1.5f;    // base*1.5
    [SerializeField] private bool useAlphaFade = true;

    private CanvasGroup canvasGroup;
    private Vector3 baseScale;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
        baseScale = transform.localScale;
    }

    public void Show()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        StartCoroutine(ShowRoutine());
    }

    public void Hide()
    {
        StopAllCoroutines();
        StartCoroutine(HideRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        Vector3 start = baseScale * startFactor;
        Vector3 peak = baseScale * peakFactor;
        Vector3 end = baseScale;
        transform.localScale = start;
        if (useAlphaFade) canvasGroup.alpha = 0f;

        float t = 0f;
        while (t < inDuration1)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / inDuration1);
            float eased = EaseOutQuad(p);
            transform.localScale = Vector3.Lerp(start, peak, eased);
            if (useAlphaFade) canvasGroup.alpha = Mathf.Lerp(0f, 1f, p);
            yield return null;
        }

        t = 0f;
        while (t < inDuration2)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / inDuration2);
            float eased = EaseOutQuad(p);
            transform.localScale = Vector3.Lerp(peak, end, eased);
            yield return null;
        }

        transform.localScale = end;
        if (useAlphaFade) canvasGroup.alpha = 1f;
    }

    private IEnumerator HideRoutine()
    {
        Vector3 start = baseScale;
        Vector3 end = baseScale * startFactor;
        float t = 0f;
        while (t < outDuration)
        {
            t += Time.unscaledDeltaTime;
            float p = Mathf.Clamp01(t / outDuration);
            float eased = EaseInQuad(p);
            transform.localScale = Vector3.Lerp(start, end, eased);
            if (useAlphaFade) canvasGroup.alpha = Mathf.Lerp(1f, 0f, p);
            yield return null;
        }
        transform.localScale = baseScale; // reset to original to guarantee exact value
        if (useAlphaFade) canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    private float EaseOutQuad(float x) => 1f - (1f - x) * (1f - x);
    private float EaseInQuad(float x) => x * x;
}
}
