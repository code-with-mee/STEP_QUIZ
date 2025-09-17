using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class QuizManager : MonoBehaviour
{
    public Text questionText;
    public Text timerText;
    public Text scoreText;
    public Button[] answerButtons;
    private System.Random rng = new System.Random();

    private List<QuizItem> items;
    private int currentIndex = -1;
    private QuizItem current;
    private Coroutine timerRoutine;
    public float initTime = 30f;
    private float timeRemaining = 30f;
    private int score = 0;

    private void OnEnable()
    {
        timeRemaining = initTime;
        items = QuizLoader.LoadFromResources("quiz");
        items = items.OrderBy(x => UnityEngine.Random.value).ToList();
        score = 0;
        scoreText.text = $"Score: {score}";
        NextQuestion();
    }

    public void NextQuestion()
    {
        if (items == null || items.Count == 0) return;
        currentIndex = (currentIndex + 1) % items.Count;
        current = items[currentIndex];

        questionText.text = current.question;

        var answers = new List<int>(current.answers);
        for (int i = answers.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (answers[i], answers[j]) = (answers[j], answers[i]);
        }

        char[] letters = { 'A', 'B', 'C', 'D' };
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            int value = answers[i];
            answerButtons[i].GetComponentInChildren<Text>().text = $"{letters[i]}. {value}";
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswer(index, value));
        }

        StartTimer();
    }

    private void StartTimer()
    {
        if (timerRoutine != null) StopCoroutine(timerRoutine);
        timerRoutine = StartCoroutine(TimerCountdown());
    }

    private IEnumerator TimerCountdown()
    {
        timeRemaining = initTime;
        while (timeRemaining > 0f)
        {
            timerText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}s";
            yield return new WaitForSeconds(1f);
            timeRemaining -= 1f;
        }
        timerText.text = "Time: 0s";
        NextQuestion();
    }

    private void OnAnswer(int buttonIndex, int chosenValue)
    {
        bool correct = chosenValue == current.correct;
        if (correct)
        {
            score++;
            scoreText.text = $"Score: {score}";
        }

        foreach (var btn in answerButtons) btn.interactable = false;
        answerButtons[buttonIndex].GetComponent<Image>().color = correct ? Color.green : Color.red;
        StartCoroutine(ResetAndNext());
    }

    private IEnumerator ResetAndNext()
    {
        yield return new WaitForSeconds(1f);
        foreach (var btn in answerButtons)
        {
            btn.interactable = true;
            btn.GetComponent<Image>().color = Color.white;
        }
        NextQuestion();
    }
}
