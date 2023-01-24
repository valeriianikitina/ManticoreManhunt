using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VictoryNotification : MonoBehaviour
{
    public GameObject victoryPanel;
    public TMP_Text victoryText;
    public int collectiblesNeeded = 3;
    private int collectiblesCollected = 0;

    public void CollectCollectible()
    {
        collectiblesCollected++;
        if (collectiblesCollected >= collectiblesNeeded)
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