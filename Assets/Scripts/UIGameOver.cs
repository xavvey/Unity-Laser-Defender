using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();        
    }

    void Start()
    {
        finalScoreText.text = "YOUR SCORE:\n" + scoreKeeper.GetCurrentScore();
    }
}
