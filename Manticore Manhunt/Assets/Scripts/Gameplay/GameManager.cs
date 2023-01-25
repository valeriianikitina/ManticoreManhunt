using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public TMP_Text movesText;

    [SerializeField]
    DefeatNotification defeat;
    [SerializeField]
    VictoryNotification victory;

    [SerializeField]
    int maxMoves = 4;
    int movesLeft = 0;

    private void Awake()
    {
        movesLeft = maxMoves;
        _instance = this;
    }

    private void Start()
    {
        movesText.text = $"Moves Left: {movesLeft}";
    }

    public void OnMove()
    {
        movesLeft--;
        movesText.text = $"Moves Left: {movesLeft}"; 
        if (movesLeft <= 0 && !victory.Won)
        {
            defeat.ShowDefeat();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
