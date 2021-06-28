using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    
    public delegate void OnGameStart();
    public static event OnGameStart GameStartEvent;

    public delegate void OnGameEnd();
    public static event OnGameEnd GameEndEvent;

    public bool isGameActive = false;

    [SerializeField] private Button button;
    
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private float time = 120;
    private bool isCount = false;

    [SerializeField] private TextMeshProUGUI gameEndScreen;

    public ShakeHandler shake;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        timer.gameObject.SetActive(false);
        
        GameStartEvent += SetGameActive;
        GameStartEvent += DestroyStartButton;
        GameStartEvent += startTimer;

        shake = GetComponent<ShakeHandler>();
    }

    private void Update()
    {
        if (isCount)
        {
            time -= Time.deltaTime;
            timer.text = Mathf.FloorToInt(time).ToString();

            if (time <= 0)
            {
                GameEndEvent();
            }
        }
    }

    private void SetGameActive()
    {
        isGameActive = true;

    }

    private void DestroyStartButton()
    {
        Destroy(button.gameObject);
    }

    private void startTimer()
    {
        isCount = true;
        timer.gameObject.SetActive(true);

        GameEndEvent += stopTimer;
        GameEndEvent += EndScreen;
    }

    private void stopTimer()
    {
        isCount = false;
        timer.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        GameStartEvent();
    }

    public void EndScreen()
    {
        gameEndScreen.gameObject.SetActive(true);
        gameEndScreen.text = "Game Over" + "\n" +
                             "Final Score: " + ScoreManager.Instance.pointScoreHander.GetScore();
    }
}
