using UnityEngine;

public class PaintingUI : MonoBehaviour
{
    public GameObject infoPanel;

    public void ToggleInfo()
    {
        infoPanel.SetActive(!infoPanel.activeSelf);
    }
}