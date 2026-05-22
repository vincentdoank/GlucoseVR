using UnityEngine;
using DG.Tweening;

public class PhoneUI : MonoBehaviour
{
    [Header("Animation Target")]
    [SerializeField] private Transform focusAnchor;

    [Header("Animation Settings")]
    [SerializeField] private float moveDuration = 0.25f;
    [SerializeField] private float scaleDuration = 0.25f;
    [SerializeField] private float rotateDuration = 0.25f;
    [SerializeField] private float scaleUpMultiplier = 1.2f;
    [SerializeField] private float frontOffset = 0.05f;

    [Header("Focus Rotation")]
    [SerializeField] private Vector3 focusRotationEuler = new Vector3(45f, 0f, 0f);

    [Header("Target Mirror")]
    [SerializeField] private GameObject targetMirror;

    private Vector3 defaultLocalPosition;
    private Vector3 defaultScale;
    private Quaternion defaultLocalRotation;

    private Tweener moveTween;
    private Tweener scaleTween;
    private Tweener rotateTween;


    private void Awake()
    {
        defaultLocalPosition = transform.localPosition;
        defaultScale = transform.localScale;
        defaultLocalRotation = transform.localRotation;
    }

    public void Focus()
    {
        if (focusAnchor == null) return;

        moveTween?.Kill();
        scaleTween?.Kill();
        rotateTween?.Kill();

        Vector3 targetLocalPos =
            focusAnchor.localPosition +
            focusAnchor.localRotation * Vector3.forward * frontOffset;

        moveTween = transform.DOLocalMove(targetLocalPos, moveDuration)
            .SetEase(Ease.OutBack).Play();

        scaleTween = transform.DOScale(
                defaultScale * scaleUpMultiplier,
                scaleDuration)
            .SetEase(Ease.OutBack).Play();

        rotateTween = transform.DOLocalRotate(
                focusRotationEuler,
                rotateDuration)
            .SetEase(Ease.OutBack).Play();

        OpenMirror();
    }

    public void Unfocus()
    {
        CloseMirror();
        moveTween?.Kill();
        scaleTween?.Kill();
        rotateTween?.Kill();

        moveTween = transform.DOLocalMove(
                defaultLocalPosition,
                moveDuration)
            .SetEase(Ease.OutBack).Play();

        scaleTween = transform.DOScale(
                defaultScale,
                scaleDuration)
            .SetEase(Ease.OutBack).Play();

        rotateTween = transform.DOLocalRotateQuaternion(
                defaultLocalRotation,
                rotateDuration)
            .SetEase(Ease.OutBack).Play();
    }

    public void SetFocusAnchor(Transform anchor)
    {
        focusAnchor = anchor;
    }

    public void OpenMirror()
    {
        if(targetMirror)
            targetMirror.SetActive(true);
    }

    public void CloseMirror()
    {
        if (targetMirror)
            targetMirror.SetActive(false);
    }
}