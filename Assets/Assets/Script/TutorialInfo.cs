using UnityEngine;

public class TutorialInfo : MonoBehaviour
{
    [Header("UI References")]
    public GameObject tutorialPanel;

    private void Start()
    {
        // Sembunyikan panel saat game dimulai
        tutorialPanel.SetActive(false);
    }

    // Dipanggil saat tombol Show Tutorial ditekan
    public void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }

    // Dipanggil saat tombol Close ditekan
    public void HideTutorial()
    {
        tutorialPanel.SetActive(false);
    }
}