using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text timeText;

    [Header("Level Time")]
    [SerializeField] private float startTimeSeconds = 100f;

    private int points;
    private int coins;
    private float timeLeft;
    private bool failedLogged;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    private void Start()
    {
        timeLeft = startTimeSeconds;
        RefreshUI();
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0) timeLeft = 0;

        if (timeLeft <= 0f && !failedLogged)
        {
            failedLogged = true;
            Debug.Log("MAMA MIA! YOU'RE OUT OF TIME!!!");
        }

        RefreshTime();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        points += amount * 100;
        RefreshUI();
    }

    public void AddPoints(int amount)
    {
        points += amount;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (pointsText) pointsText.text = $"POINTS\n{points:000000}";
        if (coinsText)  coinsText.text  = $"COINS\n{coins:00}";
        RefreshTime();
    }

    private void RefreshTime()
    {
        if (timeText) timeText.text = $"TIME\n{Mathf.CeilToInt(timeLeft):000}";
        if (timeLeft <= 0) timeText.text = "MAMA MIA! YOU'RE OUT OF TIME!!!";
    }
}