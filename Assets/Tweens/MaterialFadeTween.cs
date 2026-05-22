using UnityEngine;
using DG.Tweening;

public class MaterialFadeTween : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private string colorProperty = "_BaseColor";
    public float to = 0;
    public float duration = 0.5f;
    public int loopCount = -1;
    public Ease easeType;
    public LoopType loopType;

    public bool playOnEnable = true;

    private Material runtimeMaterial;
    private Tween currentTween;
    private Color defaultColor;

    private void OnEnable()
    {
        if (playOnEnable)
        {
            FadeTo(to, duration);
        }
    }

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponent<Renderer>();

        // Create unique material instance
        runtimeMaterial = targetRenderer.material;
        defaultColor = runtimeMaterial.GetColor(colorProperty);
    }

    public void FadeTo(float targetAlpha, float duration)
    {
        if (runtimeMaterial == null)
            return;
        runtimeMaterial.SetColor(colorProperty, defaultColor);
        currentTween?.Kill();

        Color startColor = runtimeMaterial.GetColor(colorProperty);

        currentTween = DOTween.To(
            () => startColor.a,
            alpha =>
            {
                Color c = runtimeMaterial.GetColor(colorProperty);
                c.a = alpha;
                runtimeMaterial.SetColor(colorProperty, c);
            },
            targetAlpha,
            duration
        ).SetEase(easeType).SetLoops(loopCount, loopType).Play();
    }

    public void FadeIn(float duration = 0.5f)
    {
        FadeTo(1f, duration);
    }

    public void FadeOut(float duration = 0.5f)
    {
        FadeTo(0f, duration);
    }
}