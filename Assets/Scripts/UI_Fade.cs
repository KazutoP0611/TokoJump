using System.Collections;
using UnityEngine;

public class UI_Fade : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 1.5f;

    public Coroutine fadeCoroutine {  get; private set; }

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn() => Fade(0);

    public void FadeOut() => Fade(1);

    private void Fade(float targetAlpha)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeAlpha(targetAlpha));

    }

    private IEnumerator FadeAlpha(float targetAlpha)
    {
        float time = 0;
        float startAlpha = canvasGroup.alpha;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);

            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
