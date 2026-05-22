using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;

public class VRScreenFade : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float defaultFadeDuration = 0.5f;

    private Tween currentTween;

    // One-time event callback
    public UnityEvent pendingFadeCompleteEvent;

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

    public void SetFadeCompleteEvent(UnityAction action)
    {
        pendingFadeCompleteEvent.RemoveAllListeners();
        pendingFadeCompleteEvent.AddListener(action);
        Debug.LogWarning("complete", gameObject);
    }

    private void InvokeAndClearEvent()
    {
        pendingFadeCompleteEvent?.Invoke();
        pendingFadeCompleteEvent.RemoveAllListeners();
    }

    public void FadeIn(float duration = -1f)
    {
        if (fadeImage == null) return;

        if (duration < 0f)
            duration = defaultFadeDuration;

        currentTween?.Kill();

        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        currentTween = fadeImage
            .DOFade(0f, duration)
            .SetEase(Ease.Linear)
            .OnComplete(InvokeAndClearEvent)
            .Play();
    }

    public void FadeOut(float duration = -1f)
    {
        if (fadeImage == null) return;

        if (duration < 0f)
            duration = defaultFadeDuration;

        currentTween?.Kill();

        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        currentTween = fadeImage
            .DOFade(1f, duration)
            .SetEase(Ease.Linear)
            .OnComplete(InvokeAndClearEvent)
            .Play();
    }

    public void FadeOutIn(float fadeOutDuration, float holdTime, float fadeInDuration)
    {
        if (fadeImage == null) return;

        currentTween?.Kill();

        Sequence seq = DOTween.Sequence();

        seq.Append(fadeImage.DOFade(1f, fadeOutDuration));
        seq.AppendInterval(holdTime);
        seq.Append(fadeImage.DOFade(0f, fadeInDuration));
        seq.OnComplete(InvokeAndClearEvent);

        currentTween = seq.Play();
    }
}