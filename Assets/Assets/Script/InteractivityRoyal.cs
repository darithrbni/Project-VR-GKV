using UnityEngine;
using DG.Tweening;

public class InteractivityRoyal : MonoBehaviour
{
    [Header("References")]
    public GameObject redWalls;
    public Transform redCarpet;

    [Header("Wall Movement")]
    public float wallMoveDistance = 3f;
    public float wallMoveDuration = 1f;

    [Header("Audio")]
    public AudioSource trumpetSFX;

    [Header("Carpet Movement")]
    public float moveDistance = 2f;
    public float moveDuration = 1f;

    private Vector3 carpetOriginalPosition;

    private Vector3 wallOriginalPosition;

    private bool playerInside = false;

    private void Awake()
    {
        carpetOriginalPosition = redCarpet.position;
        wallOriginalPosition = redWalls.transform.position;
    }

    public void OnPlayerEnterZone()
    {
        playerInside = true;

        redWalls.transform.DOKill();

        redWalls.transform.DOMoveY(
                wallOriginalPosition.y - wallMoveDistance,
                wallMoveDuration)
            .SetEase(Ease.OutCubic);

        // Mainkan SFX
        trumpetSFX?.Play();

        // Hentikan tween lama
        redCarpet.DOKill();

        // Gerakkan karpet ke atas
        redCarpet.DOMoveY(
                carpetOriginalPosition.y + moveDistance,
                moveDuration)
            .SetEase(Ease.OutBack);
    }

    public void OnPlayerExitZone()
    {
        playerInside = false;

        // Hentikan tween lama
        redCarpet.DOKill();

        // Turunkan karpet
        redCarpet.DOMoveY(
                carpetOriginalPosition.y,
                moveDuration)
            .SetEase(Ease.InBack);

        redWalls.transform.DOKill();

        redWalls.transform.DOMoveY(
                wallOriginalPosition.y,
                wallMoveDuration)
            .SetEase(Ease.InSine);
    }
}