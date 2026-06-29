using UnityEngine;
using DG.Tweening;

public class InteractivityGuitar : MonoBehaviour
{
    [Header("Parent Object")]
    public Transform guitarParent;

    [Header("Movement")]
    public float moveDistance = 4f;
    public float moveDuration = 2f;

    [Header("Audio")]
    public AudioSource fallSFX;

    private Transform[] guitars;
    private Vector3[] originalPositions;

    private void Awake()
    {
        // Pastikan parent sudah diisi
        if (guitarParent == null)
        {
            Debug.LogError("Guitar Parent belum diisi!", this);
            return;
        }

        int childCount = guitarParent.childCount;

        guitars = new Transform[childCount];
        originalPositions = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            guitars[i] = guitarParent.GetChild(i);
            originalPositions[i] = guitars[i].position;
        }
    }

    public void OnPlayerEnterZone()
    {
        DOVirtual.DelayedCall(0.5f, () =>
        {
            fallSFX?.Play();
        });

        for (int i = 0; i < guitars.Length; i++)
        {
            guitars[i].DOKill();

            guitars[i]
                .DOMoveY(
                    originalPositions[i].y - moveDistance,
                    moveDuration)
                .SetEase(Ease.OutBounce);
        }
    }

    public void OnPlayerExitZone()
    {
        for (int i = 0; i < guitars.Length; i++)
        {
            guitars[i].DOKill();

            guitars[i]
                .DOMoveY(
                    originalPositions[i].y,
                    moveDuration)
                .SetEase(Ease.OutSine);
        }
    }
}