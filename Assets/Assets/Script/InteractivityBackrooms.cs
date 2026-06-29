using UnityEngine;
using DG.Tweening;

public class BackroomsSecret : MonoBehaviour
{
    [Header("Floor")]
    public Transform targetFloor;

    [Header("Audio")]
    public AudioSource normalBGM;
    public AudioSource backroomsBGM;

    [Header("Jumpscare Audio")]
    public AudioSource boomSFX;

    [Header("Settings")]
    public int requiredClicks = 3;
    public float floorMoveDistance = 100f;
    public float floorMoveDuration = 0.3f;

    private int currentClicks = 0;
    private bool activated = false;

    public void OnPaintingClicked()
    {
        if (activated)
            return;

        currentClicks++;

        Debug.Log("Backrooms Click: " + currentClicks);

        if (currentClicks >= requiredClicks)
        {
            ActivateBackrooms();
        }
    }

    private void ActivateBackrooms()
    {
        activated = true;

        boomSFX?.Play();

        Vector3 direction;

        bool useX = Random.value > 0.5f;

        if (useX)
        {
            direction =
                (Random.value > 0.5f)
                ? Vector3.right
                : Vector3.left;
        }
        else
        {
            direction =
                (Random.value > 0.5f)
                ? Vector3.forward
                : Vector3.back;
        }

        targetFloor
            .DOMove(
                targetFloor.position +
                direction * floorMoveDistance,
                floorMoveDuration)
            .SetEase(Ease.OutExpo);

        // Ganti BGM
        normalBGM
            .DOFade(0f, 2f)
            .OnComplete(() =>
            {
                normalBGM.Stop();
            });

        backroomsBGM.volume = 0f;
        backroomsBGM.Play();

        backroomsBGM
            .DOFade(1f, 2f);
    }
}