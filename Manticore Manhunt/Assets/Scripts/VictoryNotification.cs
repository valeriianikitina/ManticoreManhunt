using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class VictoryNotification : MonoBehaviour
{
    public GameObject victoryPanel;
    public TMP_Text victoryText;
    public int collectiblesNeeded = 3;
    private List<Vector2Int> collectiblesCollected = new();
    public void UncollectCollectible(Vector2Int coord)
    {
        collectiblesCollected.Remove(coord);
    }

    public bool Won => collectiblesCollected.Distinct().Count() >= collectiblesNeeded;

    public void CollectCollectible(Vector2Int coord)
    {
        collectiblesCollected.Add(coord);
        if (Won)
        {
            ShowVictory();
        }
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        victoryText.text = "The Victory is ours, hermanos.";
    }
}