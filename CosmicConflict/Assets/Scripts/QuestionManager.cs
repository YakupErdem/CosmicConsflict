using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Question
{
    public string prompt;
    public string[] choices = new string[4];
    public int correctIndex;
}

public class QuestionManager : MonoBehaviour
{
    public static QuestionManager instance;

    [Header("Questions")]
    [SerializeField] private Question[] questions;

    [Header("UI")]
    [SerializeField] private GameObject panel;
    [SerializeField] private Text questionLabel;
    [SerializeField] private Text[] choiceLabels = new Text[4];
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private Text popupText;
    [SerializeField] private float popupDuration = 1.5f;
    [SerializeField] private UI.PopupAnimator questionPopupAnimator;
    [SerializeField] private UI.PopupAnimator popupAnimator;

    private int nextIndex = 0;
    private int currentIndex = -1;
    private bool isOpen;

    private void Awake()
    {
        instance = this;
        if (panel != null) panel.SetActive(false);
        if (panel != null && questionPopupAnimator == null) questionPopupAnimator = panel.GetComponent<UI.PopupAnimator>() ?? panel.AddComponent<UI.PopupAnimator>();
        if (popupPanel != null && popupAnimator == null) popupAnimator = popupPanel.GetComponent<UI.PopupAnimator>() ?? popupPanel.AddComponent<UI.PopupAnimator>();
        if (FindObjectOfType<UI.TimeScaleSmoother>() == null) gameObject.AddComponent<UI.TimeScaleSmoother>();
    }

    public bool HasRemainingQuestions()
    {
        return nextIndex < (questions != null ? questions.Length : 0);
    }

    public bool TryOpenQuestion()
    {
        if (!HasRemainingQuestions()) return false;
        currentIndex = nextIndex;
        nextIndex++;
        var q = questions[currentIndex];
        if (questionPopupAnimator != null) questionPopupAnimator.Show();
        if (questionLabel != null) questionLabel.text = q.prompt;
        for (int i = 0; i < 4 && i < choiceLabels.Length; i++)
        {
            if (choiceLabels[i] != null)
            {
                choiceLabels[i].text = q.choices != null && q.choices.Length > i ? q.choices[i] : "";
            }
        }
        isOpen = true;
        UI.TimeScaleSmoother.instance.PauseSmooth(0.5f);
        return true;
    }

    public void Answer(int answer)
    {
        if (!isOpen) return;
        var correct = currentIndex >= 0 && currentIndex < questions.Length && answer == questions[currentIndex].correctIndex;
        if (correct && GameManager.instance != null)
        {
            GameManager.instance.AddPoint(30);
        }
        Close();
        var message = correct ? "Doğru bildin" : "Yanlış bildin";
        ShowPopup(message);
    }

    private void Close()
    {
        isOpen = false;
        if (questionPopupAnimator != null) questionPopupAnimator.Hide();
        UI.TimeScaleSmoother.instance.ResumeSmooth(0.5f);
    }

    private void ShowPopup(string message)
    {
        if (popupAnimator != null) popupAnimator.Show();
        if (popupText != null) popupText.text = message;
        StartCoroutine(HidePopup());
    }

    private System.Collections.IEnumerator HidePopup()
    {
        yield return new WaitForSecondsRealtime(popupDuration);
        if (popupAnimator != null) popupAnimator.Hide();
    }
}
