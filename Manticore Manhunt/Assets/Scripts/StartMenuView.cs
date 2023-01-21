using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuView : View
{
    [SerializeField] private Button _startButton;
    public override void Initialize()
    {
        _startButton.onClick.AddListener(
            () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
    }
}


