using UnityEngine;
using DG.Tweening;

public class PhoneUI : MonoBehaviour
{
    [Header("Animation Target")]
    [SerializeField] private Transform focusAnchor;

    [Header("Animation Settings")]
    [SerializeField] private float moveDuration = 0.25f;
    [SerializeField] private float scaleDuration = 0.25f;
    [SerializeField] private float scaleUpMultiplier = 1.2f;
    [SerializeField] private float frontOffset = 0.05f;

    private Vector3 defaultLocalPosition;
    private Vector3 defaultScale;

    private Tweener moveTween;
    private Tweener scaleTween;

    private void Awake()
    {
        defaultLocalPosition = transform.localPosition;
        defaultScale = transform.localScale;
    }

    public void Focus()
    {
        if (focusAnchor == null) return;

        moveTween?.Kill();
        scaleTween?.Kill();

        Vector3 targetLocalPos =
            focusAnchor.localPosition +
            focusAnchor.localRotation * Vector3.forward * frontOffset;

        moveTween = transform.DOLocalMove(targetLocalPos, moveDuration)
            .SetEase(Ease.OutBack)
            .Play();

        scaleTween = transform.DOScale(
                defaultScale * scaleUpMultiplier,
                scaleDuration)
            .SetEase(Ease.OutBack)
            .Play();
    }

    public void Unfocus()
    {
        moveTween?.Kill();
        scaleTween?.Kill();

        moveTween = transform.DOLocalMove(
                defaultLocalPosition,
                moveDuration)
            .SetEase(Ease.OutBack).Play()   ;

        scaleTween = transform.DOScale(
                defaultScale,
                scaleDuration)
            .SetEase(Ease.OutBack).Play();
    }

    public void SetFocusAnchor(Transform anchor)
    {
        focusAnchor = anchor;
    }
}