using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    int coin;
    int point;
    int score;
    public TextMeshProUGUI scoreText;
    public UnityEvent onFinished;

    public int Coin { get => coin; set => coin = value; }

    private void Awake()
    {
        coin = 0;
        point = 0;
        score = 0;

    }

    void Start()
    {
        scoreText.text = score.ToString();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            coin++;
            score += 10;
            scoreText.text = score.ToString();
        }
    }

    public void OnFinish()
    {
        onFinished.Invoke();
    }
}
