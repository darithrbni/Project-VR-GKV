using UnityEngine;
using DG.Tweening;

public class InteractivityWater : MonoBehaviour
{
    [Header("References")]
    public GameObject waterSurface;
    public AudioSource waterAudio;

    [Header("Movement")]
    public float riseAmount = 1f;
    public float moveDuration = 3f;

    [Header("Audio")]
    public float fadeDuration = 2f;
    public float targetVolume = 1f;

    private Vector3 originalPosition;

    private Tween moveTween;
    private Tween audioTween;

    private bool playerInside = false;

    private void Awake()
    {
        originalPosition = waterSurface.transform.position;

        // Matikan air di awal
        waterSurface.SetActive(false);

        if (waterAudio != null)
        {
            waterAudio.volume = 0f;
            waterAudio.loop = true;
            waterAudio.Stop();
        }
    }

    public void OnPlayerEnterZone()
    {
        playerInside = true;

        moveTween?.Kill();
        audioTween?.Kill();

        // Aktifkan air
        if (!waterSurface.activeSelf)
            waterSurface.SetActive(true);

        // Pastikan posisi benar sebelum bergerak
        waterSurface.transform.position = originalPosition;

        // Gerakkan air ke atas
        moveTween = waterSurface.transform
            .DOMoveY(
                originalPosition.y + riseAmount,
                moveDuration)
            .SetEase(Ease.InOutSine);

        // Audio fade in
        if (waterAudio != null)
        {
            if (!waterAudio.isPlaying)
                waterAudio.Play();

            audioTween = waterAudio
                .DOFade(targetVolume, fadeDuration);
        }
    }

    public void OnPlayerExitZone()
    {
        playerInside = false;

        moveTween?.Kill();
        audioTween?.Kill();

        // Turunkan air
        moveTween = waterSurface.transform
            .DOMoveY(
                originalPosition.y,
                moveDuration)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                // Jika player belum masuk lagi
                if (!playerInside)
                {
                    waterSurface.SetActive(false);
                }
            });

        // Audio fade out
        if (waterAudio != null)
        {
            audioTween = waterAudio
                .DOFade(0f, fadeDuration)
                .OnComplete(() =>
                {
                    waterAudio.Stop();
                });
        }
    }
}