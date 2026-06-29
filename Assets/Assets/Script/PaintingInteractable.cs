using UnityEngine;
using UnityEngine.Events;

public class PaintingInteractable : MonoBehaviour
{
    [Header("UI References")]
    public GameObject showInfoButton;
    public GameObject infoPanel;

    [Header("Easter Egg Event")]
    public UnityEvent onPlayerEnterZone;

    [Header("Exit Zone Event")]
    public UnityEvent onPlayerExitZone;

    private void Start()
    {
        showInfoButton.SetActive(true);
        infoPanel.SetActive(false);
    }

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<CharacterController>() != null)
        {
            onPlayerEnterZone?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<CharacterController>() != null)
        {
            onPlayerExitZone?.Invoke();
        }
    }
}