using UnityEngine;

public class PaintingInteraction : MonoBehaviour
{
    [Header("References")]
    public GameObject canvasUI;
    public GameObject infoPanel;
    public GameObject showInfoButton;

    private void Start()
    {
        canvasUI.SetActive(false);
        infoPanel.SetActive(false);
        showInfoButton.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Masuk trigger: " + other.name);

        // Jika yang masuk adalah player
        if (other.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>() != null)
        {
            canvasUI.SetActive(true);

            infoPanel.SetActive(false);
            showInfoButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Keluar trigger: " + other.name);

        if (other.GetComponentInParent<Unity.XR.CoreUtils.XROrigin>() != null)
        {
            canvasUI.SetActive(false);

            infoPanel.SetActive(false);
            showInfoButton.SetActive(true);
        }
    }

    public void ShowInfo()
    {
        showInfoButton.SetActive(false);
        infoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        showInfoButton.SetActive(true);
    }
}