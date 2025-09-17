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
    public GameObject gameOverPanel;
    public Text gameOverText;
    public Button retryButton;
    public Button levelButton;
    public ScreenManager screenManager;

    private System.Random rng = new System.Random();
    private List<QuizItem> items;
    private int currentIndex = -1;
    private QuizItem current;
    private Coroutine timerRoutine;
    public float initTime = 30f;
    private float timeRemaining = 30f;
    private int score = 0;
    private int maxQuestions = 20;
    private int answered = 0;

    private void OnEnable()
    {
        timeRemaining = initTime;
        items = QuizLoader.LoadFromResources("quiz").OrderBy(x => UnityEngine.Random.value).Take(maxQuestions).ToList();
        score = 0;
        answered = 0;
        scoreText.text = $"{score}/{maxQuestions}";
        gameOverPanel.SetActive(false);
        NextQuestion();
        retryButton.onClick.RemoveAllListeners();
        retryButton.onClick.AddListener(RestartGame);
        levelButton.onClick.RemoveAllListeners();
    }

    public void NextQuestion()
    {
        if (items == null || items.Count == 0) return;
        if (answered >= maxQuestions) { FinishRun(); return; }

        currentIndex = answered % items.Count;
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
            answerButtons[i].interactable = true;
            answerButtons[i].GetComponent<Image>().color = Color.white;
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
        Lose("Time up!");
    }

    private void OnAnswer(int buttonIndex, int chosenValue)
    {
        bool correct = chosenValue == current.correct;
        foreach (var btn in answerButtons) btn.interactable = false;
        answerButtons[buttonIndex].GetComponent<Image>().color = correct ? Color.green : Color.red;

        if (!correct)
        {
            StopTimer();
            StartCoroutine(ShowLoseThenPanel("Wrong answer!"));
            return;
        }

        score++;
        answered++;
        scoreText.text = $"{score}/{maxQuestions}";
        StopTimer();
        StartCoroutine(NextAfterDelay());
    }

    private IEnumerator NextAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        NextQuestion();
    }

    private void StopTimer()
    {
        if (timerRoutine != null)
        {
            StopCoroutine(timerRoutine);
            timerRoutine = null;
        }
    }

    private void Lose(string reason)
    {
        StopTimer();
        foreach (var btn in answerButtons)
        {
            btn.interactable = false;
        }
        StartCoroutine(ShowLoseThenPanel(reason));
    }

    private IEnumerator ShowLoseThenPanel(string reason)
    {
        yield return new WaitForSeconds(0.6f);
        gameOverText.text = $"{reason}\nScore: {score}/{maxQuestions}";
        gameOverPanel.SetActive(true);
    }

    private void FinishRun()
    {
        StopTimer();
        foreach (var btn in answerButtons)
        {
            btn.interactable = false;
        }
        gameOverText.text = $"Finished!\nScore: {score}/{maxQuestions}";
        gameOverPanel.SetActive(true);
    }

    private void RestartGame()
    {
        gameOverPanel.SetActive(false);
        timeRemaining = initTime;
        items = QuizLoader.LoadFromResources("quiz").OrderBy(x => UnityEngine.Random.value).Take(maxQuestions).ToList();
        score = 0;
        answered = 0;
        scoreText.text = $"{score}/{maxQuestions}";
        NextQuestion();
    }
}
