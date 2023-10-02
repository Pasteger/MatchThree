using UnityEngine;
using UnityEngine.UI;

public class ScoreCalculator : MonoBehaviour
{
    public int Score { private set; get; }
    public int Meteorites { private set; get; }
    public int Ice { private set; get; }
 
    public GameObject meteorites;

    
    public Text meteoritesText;
    public Text scoreText;
    
    private void Awake()
    {
        Meteorites = meteorites.transform.childCount;
    }

    public void AddScore(int score)
    {
        Score += score;
    }

    private void Update()
    {
        scoreText.text = Score.ToString();
        meteoritesText.text = meteorites.transform.childCount.ToString();
    }
}
