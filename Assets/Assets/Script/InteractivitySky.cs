using UnityEngine;
using DG.Tweening;

public class InteractivitySky : MonoBehaviour
{
    public float moveDistance = 8f;
    public float duration = 3f;

    private Vector3 originalPosition;
    private Tween currentTween;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    public void OnPlayerEnterZone()
    {
        currentTween?.Kill();

        currentTween = transform.DOMoveZ(
            originalPosition.z - moveDistance,
            duration)
            .SetEase(Ease.InOutSine);
    }

    public void OnPlayerExitZone()
    {
        currentTween?.Kill();

        currentTween = transform.DOMove(
            originalPosition,
            duration)
            .SetEase(Ease.InOutSine);
    }
}