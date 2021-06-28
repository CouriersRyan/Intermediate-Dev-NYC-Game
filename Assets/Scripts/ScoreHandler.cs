using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] private string _scoreText = "Score: ";
    [SerializeField] private int _scoreInt = 0;
    
    private TextMeshProUGUI _tmpScore;
    // Start is called before the first frame update
    void Start()
    {
        _tmpScore = GetComponent<TextMeshProUGUI>();
        _tmpScore.text = _scoreText + _scoreInt;
    }


    public void UpdateScore(int deltaScore)
    {
        _scoreInt += deltaScore;
        _tmpScore.text = _scoreText + _scoreInt;
    }

    public int GetScore()
    {
        return _scoreInt;
    }
}
