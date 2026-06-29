using UnityEngine;
using DG.Tweening;

public class InteractivityBoat : MonoBehaviour
{
    [Header("Rise")]
    public float riseDistance = 2f;
    public float riseDuration = 2f;

    [Header("Horizontal Floating")]
    public float swayDistance = 0.3f;
    public float swayDuration = 3f;

    [Header("Vertical Floating")]
    public float bobHeight = 0.1f;
    public float bobDuration = 2f;

    [Header("Rotation")]
    public float rotationAngle = 5f;
    public float rotationDuration = 2f;

    private Vector3 originalPosition;
    private Vector3 floatingPosition;
    private Vector3 originalRotation;

    private bool playerInside = false;

    private void Awake()
    {
        originalPosition = transform.position;
        originalRotation = transform.eulerAngles;
    }

    public void OnPlayerEnterZone()
    {
        playerInside = true;

        transform.DOKill();

        // Naik ke permukaan air
        transform.DOMoveY(
                originalPosition.y + riseDistance,
                riseDuration)
            .SetEase(Ease.OutSine)
            .OnComplete(() =>
            {
                if (!playerInside)
                    return;

                floatingPosition = transform.position;
                StartFloating();
            });
    }

    private void StartFloating()
    {
        // =========================
        // GERAK KIRI KANAN
        // P -> A -> B -> A -> B ...
        // =========================

        transform.DOMoveX(
                floatingPosition.x - swayDistance,
                swayDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                transform.DOMoveX(
                        floatingPosition.x + swayDistance,
                        swayDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetId("BoatSway");
            });


        // =========================
        // NAIK TURUN
        // =========================

        transform.DOMoveY(
                floatingPosition.y + bobHeight,
                bobDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId("BoatBob");


        // =========================
        // ROTASI KIRI KANAN
        // P -> A -> B -> A -> B ...
        // =========================

        transform.DORotate(
                new Vector3(
                    originalRotation.x - rotationAngle,
                    originalRotation.y,
                    originalRotation.z),
                rotationDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                transform.DORotate(
                        new Vector3(
                            originalRotation.x + rotationAngle,
                            originalRotation.y,
                            originalRotation.z),
                        rotationDuration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo)
                    .SetId("BoatRotate");
            });
    }

    public void OnPlayerExitZone()
    {
        playerInside = false;

        DOTween.Kill("BoatSway");
        DOTween.Kill("BoatBob");
        DOTween.Kill("BoatRotate");

        transform.DOKill();

        // Kembali ke posisi awal
        transform.DOMove(
                originalPosition,
                riseDuration)
            .SetEase(Ease.InOutSine);

        // Kembali ke rotasi awal
        transform.DORotate(
                originalRotation,
                riseDuration)
            .SetEase(Ease.InOutSine);
    }
}