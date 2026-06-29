using UnityEngine;
using DG.Tweening;

public class InteractivityLotus : MonoBehaviour
{
    [Header("References")]
    public GameObject lotusGroup;

    [Header("Rise Movement")]
    public float riseAmount = 1f;
    public float riseDuration = 3f;

    [Header("Floating")]
    public float floatHeight = 0.1f;
    public float floatDuration = 2f;

    [Header("Random Float Start Delay")]
    public float minFloatDelay = 0f;
    public float maxFloatDelay = 2f;

    private Transform[] lotuses;
    private Vector3[] originalPositions;

    private bool playerInside = false;

    private void Awake()
    {
        int count = lotusGroup.transform.childCount;

        lotuses = new Transform[count];
        originalPositions = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            lotuses[i] = lotusGroup.transform.GetChild(i);
            originalPositions[i] = lotuses[i].position;
        }

        // Matikan di awal untuk mengurangi lag
        lotusGroup.SetActive(false);
    }

    public void OnPlayerEnterZone()
    {
        playerInside = true;

        if (!lotusGroup.activeSelf)
            lotusGroup.SetActive(true);

        for (int i = 0; i < lotuses.Length; i++)
        {
            int index = i;

            DOTween.Kill("LotusFloat" + index);
            lotuses[index].DOKill();

            // Pastikan posisi selalu reset
            lotuses[index].position = originalPositions[index];

            lotuses[index]
                .DOMoveY(
                    originalPositions[index].y + riseAmount,
                    riseDuration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    if (!playerInside)
                        return;

                    float randomDelay =
                        Random.Range(
                            minFloatDelay,
                            maxFloatDelay);

                    DOVirtual.DelayedCall(
                        randomDelay,
                        () =>
                        {
                            if (!playerInside)
                                return;

                            StartFloating(index);
                        });
                });
        }
    }

    private void StartFloating(int index)
    {
        lotuses[index]
            .DOMoveY(
                lotuses[index].position.y + floatHeight,
                floatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId("LotusFloat" + index);
    }

    public void OnPlayerExitZone()
    {
        playerInside = false;

        for (int i = 0; i < lotuses.Length; i++)
        {
            int index = i;

            DOTween.Kill("LotusFloat" + index);
            lotuses[index].DOKill();

            lotuses[index]
                .DOMoveY(
                    originalPositions[index].y,
                    riseDuration)
                .SetEase(Ease.InOutSine);
        }

        // Nonaktifkan group setelah semua animasi selesai
        DOVirtual.DelayedCall(riseDuration, () =>
        {
            if (!playerInside)
                lotusGroup.SetActive(false);
        });
    }
}