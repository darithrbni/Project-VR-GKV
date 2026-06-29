using UnityEngine;
using DG.Tweening;

public class InteractivityPeople : MonoBehaviour
{
    [Header("Movement")]
    public float moveDuration = 3f;

    [Header("Floating")]
    public float floatHeight = 0.5f;
    public float floatDuration = 2f;

    [Header("Random Delay")]
    public float minDelay = 0.5f;
    public float maxDelay = 2f;

    [Header("Random Distance")]
    public float minMoveDistance = 3f;
    public float maxMoveDistance = 4f;

    private Transform[] statues;

    private Vector3[] originalPositions;
    private float[] randomDistances;
    private float[] randomDelays;

    private bool playerInside = false;

    private void Awake()
    {
        int count = transform.childCount;

        statues = new Transform[count];
        originalPositions = new Vector3[count];

        randomDistances = new float[count];
        randomDelays = new float[count];

        for (int i = 0; i < count; i++)
        {
            statues[i] = transform.GetChild(i);
            originalPositions[i] = statues[i].position;
        }
    }

    public void OnPlayerEnterZone()
    {
        playerInside = true;

        for (int i = 0; i < statues.Length; i++)
        {
            int index = i;

            // Hentikan semua tween lama
            DOTween.Kill("Float" + index);
            statues[index].DOKill();

            // Generate nilai baru
            randomDelays[index] =
                Random.Range(minDelay, maxDelay);

            randomDistances[index] =
                Random.Range(minMoveDistance, maxMoveDistance);

            DOVirtual.DelayedCall(
                randomDelays[index],
                () =>
                {
                    // Jika player sudah keluar sebelum delay selesai
                    if (!playerInside)
                        return;

                    PeopleAudioHolder holder =
                        statues[index].GetComponent<PeopleAudioHolder>();

                    holder?.fallAudio?.Play();

                    statues[index]
                        .DOMoveY(
                            originalPositions[index].y
                            - randomDistances[index],
                            moveDuration)
                        .SetEase(Ease.OutSine)
                        .OnComplete(() =>
                        {
                            // Jika player keluar saat animasi berjalan
                            if (!playerInside)
                                return;

                            StartFloating(index);
                        });
                });
        }
    }

    private void StartFloating(int index)
    {
        statues[index]
            .DOMoveY(
                statues[index].position.y + floatHeight,
                floatDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetId("Float" + index);
    }

    public void OnPlayerExitZone()
    {
        playerInside = false;

        for (int i = 0; i < statues.Length; i++)
        {
            int index = i;

            // Hentikan floating
            DOTween.Kill("Float" + index);

            // Hentikan tween lain
            statues[index].DOKill();

            DOVirtual.DelayedCall(
                randomDelays[index],
                () =>
                {
                    PeopleAudioHolder holder =
                        statues[index].GetComponent<PeopleAudioHolder>();

                    holder?.riseAudio?.Play();

                    statues[index]
                        .DOMoveY(
                            originalPositions[index].y,
                            moveDuration)
                        .SetEase(Ease.InSine);
                });
        }
    }
}