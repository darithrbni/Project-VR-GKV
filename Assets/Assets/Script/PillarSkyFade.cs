using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarSkyFade : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 2f;

    private Renderer[] renderers;

    private Coroutine currentFade;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        // Mulai dalam keadaan tidak terlihat
        SetAlphaInstant(0f);
    }

    public void FadeIn()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeRoutine(1f));
    }

    public void FadeOut()
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeRoutine(0f));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        List<Material> materials = new List<Material>();

        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                materials.Add(mat);
            }
        }

        float startAlpha = materials[0].color.a;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;

            float alpha = Mathf.Lerp(
                startAlpha,
                targetAlpha,
                elapsed / fadeDuration
            );

            foreach (Material mat in materials)
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }

            yield return null;
        }

        foreach (Material mat in materials)
        {
            Color color = mat.color;
            color.a = targetAlpha;
            mat.color = color;
        }
    }

    private void SetAlphaInstant(float alpha)
    {
        foreach (Renderer rend in renderers)
        {
            foreach (Material mat in rend.materials)
            {
                Color color = mat.color;
                color.a = alpha;
                mat.color = color;
            }
        }
    }
}