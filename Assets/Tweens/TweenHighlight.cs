using UnityEngine;
using DG.Tweening;

public class TweenHighlight : MonoBehaviour
{
    [Header("Highlight Settings")]
    public Color highlightColor = Color.yellow;
    public float fadeDuration = 0.3f;
    public bool autoPlay;

    private Material[] mats;
    private Color originalColor;
    private Tween currentTween;

    private void Awake()
    {
        // Get a unique copy of the material
        mats = GetComponent<Renderer>().materials;
        foreach (Material mat in mats)
        {
            originalColor = mat.color;
        }
    }

    private void OnEnable()
    {
        if (autoPlay)
            Highlight();
    }

    public void Highlight()
    {
        // Kill previous tween if running
        currentTween?.Kill();

        // Fade into highlight color
        foreach (Material mat in mats)
        {
            currentTween = mat
                .DOColor(highlightColor, fadeDuration)
                .SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo).Play();
        }
    }
}
