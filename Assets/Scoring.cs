using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    [SerializeField] private Transform characterTransform;

    [SerializeField] private Text scoreSentence;
    private static int _score;
    private string _currentScene;

    private void Start()
    {
        _currentScene = SceneManager.GetActiveScene().name;
        if (_currentScene == "GameOverScene")
            scoreSentence.text = GetSentenceString();
    }

    private void Update()
    {
        if (_currentScene != "SampleScene") return;

        _score = (int) Math.Floor(characterTransform.position.x);
        scoreSentence.text = GetSentenceString();
    }

    private static string GetSentenceString()
    {
        return "Distance: " + _score;
    }
}
