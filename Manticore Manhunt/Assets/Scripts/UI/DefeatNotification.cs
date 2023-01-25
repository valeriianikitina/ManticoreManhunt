using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DefeatNotification : MonoBehaviour
{
    public GameObject defeatPanel;
    public TMP_Text defeatText;

    public void ShowDefeat()
    {
        defeatPanel.SetActive(true);
        defeatText.text = "Bad luck, hermano.";
    }
}