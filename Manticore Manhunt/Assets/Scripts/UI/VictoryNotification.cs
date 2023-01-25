using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class VictoryNotification : MonoBehaviour
{
    public GameObject victoryPanel;
    public TMP_Text victoryText;
    public TMP_Text flowersText;
    public int collectiblesNeeded = 3;
    private List<Vector2Int> collectiblesCollected = new();
    public void UncollectCollectible(Vector2Int coord)
    {
        collectiblesCollected.Remove(coord);
        UpdateFlowersText();
    }

    private void UpdateFlowersText()
    {
        flowersText.text = $"Flowers Collected: {collectiblesCollected.Distinct().Count()}/{collectiblesNeeded}";
    }

    public bool Won => collectiblesCollected.Distinct().Count() >= collectiblesNeeded;

    public void CollectCollectible(Vector2Int coord)
    {
        collectiblesCollected.Add(coord);
        UpdateFlowersText();
        if (Won)
        {
            ShowVictory();
        }
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);
        victoryText.text = "The Victory is ours, hermano.";
    }
}