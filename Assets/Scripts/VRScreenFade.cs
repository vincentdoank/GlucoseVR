using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VRScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float defaultFadeDuration = 0.5f;

    private Tween currentTween;

    public static VRScreenFade Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 0f;
            fadeImage.color = c;
        }
    }

    public void FadeIn(float duration = -1f)
    {
        if (fadeImage == null) return;

        if (duration < 0f)
            duration = defaultFadeDuration;

        currentTween?.Kill();

        currentTween = fadeImage
            .DOFade(0f, duration)
            .SetEase(Ease.Linear).Play();
    }

    public void FadeOut(float duration = -1f)
    {
        if (fadeImage == null) return;

        if (duration < 0f)
            duration = defaultFadeDuration;

        currentTween?.Kill();

        currentTween = fadeImage
            .DOFade(1f, duration)
            .SetEase(Ease.Linear).Play();
    }

    public void FadeOutIn(float fadeOutDuration, float holdTime, float fadeInDuration)
    {
        if (fadeImage == null) return;

        currentTween?.Kill();

        Sequence seq = DOTween.Sequence();

        seq.Append(fadeImage.DOFade(1f, fadeOutDuration)).Play();
        seq.AppendInterval(holdTime).Play();
        seq.Append(fadeImage.DOFade(0f, fadeInDuration)).Play();

        currentTween = seq;
    }
}