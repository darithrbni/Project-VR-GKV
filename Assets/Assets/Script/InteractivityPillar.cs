using System.Collections;
using UnityEngine;
using DG.Tweening;

public class InteractivityPillar : MonoBehaviour
{
    [Header("Movement")]
    public float moveDistance = 10f;
    public float moveDuration = 2f;

    [Header("Audio")]
    public AudioSource fallAudio;
    public AudioSource riseAudio;

    [Header("Delay")]
    public float movementDelay = 1f;
    public float soundDelay = 1f;

    private Transform[] pillars;
    private Vector3[] originalPositions;

    private void Awake()
    {
        int count = transform.childCount;

        pillars = new Transform[count];
        originalPositions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            pillars[i] = transform.GetChild(i);
            originalPositions[i] = pillars[i].position;
        }
    }

    public void OnPlayerEnterZone()
    {
        StopAllCoroutines();
        StartCoroutine(DropPillarsRoutine());
    }

    IEnumerator DropPillarsRoutine()
    {
        // Jadwalkan suara
        DOVirtual.DelayedCall(soundDelay, () =>
        {
            fallAudio?.Play();
        });

        // Tunggu sebelum animasi
        yield return new WaitForSeconds(movementDelay);

        // Semua pilar jatuh bersamaan
        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].DOKill();

            pillars[i]
                .DOMoveY(
                    originalPositions[i].y - moveDistance,
                    moveDuration)
                .SetEase(Ease.OutBounce);
        }
    }

    public void OnPlayerExitZone()
    {
        StopAllCoroutines();
        StartCoroutine(RaisePillarsRoutine());
    }

    IEnumerator RaisePillarsRoutine()
    {
        DOVirtual.DelayedCall(soundDelay, () =>
        {
            riseAudio?.Play();
        });

        yield return new WaitForSeconds(movementDelay);

        for (int i = 0; i < pillars.Length; i++)
        {
            pillars[i].DOKill();

            pillars[i]
                .DOMoveY(
                    originalPositions[i].y,
                    moveDuration)
                .SetEase(Ease.InBounce);
        }
    }
}