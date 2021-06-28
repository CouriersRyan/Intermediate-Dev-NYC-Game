using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreHandler crashScoreHandler;

    public ScoreHandler pointScoreHander;

    public static ScoreManager Instance;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        crashScoreHandler.gameObject.SetActive(false);
        pointScoreHander.gameObject.SetActive(false);
        GameManager.GameStartEvent += ActivateScoreHandlers;
    }

    public void UpdateCrash(int i)
    {
        crashScoreHandler.UpdateScore(i);
    }

    public void UpdatePoint(int i)
    {
        pointScoreHander.UpdateScore(i);
    }

    public void ActivateScoreHandlers()
    {
        crashScoreHandler.gameObject.SetActive(true);
        pointScoreHander.gameObject.SetActive(true);

        GameManager.GameEndEvent += StopScoreHandlers;
    }

    public void StopScoreHandlers()
    {
        crashScoreHandler.gameObject.SetActive(false);
        pointScoreHander.gameObject.SetActive(false);
    }
}
