using TMPro;
using UnityEngine;

public class VRRotateGuide : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform vrCamera;
    [SerializeField] private Transform targetObject;

    [Header("UI")]
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private TMP_Text guideText;

    [Header("Settings")]
    [SerializeField] private float visibleAngleThreshold = 15f;

    private void Update()
    {
        if (vrCamera == null || targetObject == null)
        {
            HideAll();
            return;
        }

        UpdateDirectionGuide();
    }

    private void UpdateDirectionGuide()
    {
        Vector3 directionToTarget = targetObject.position - vrCamera.position;
        directionToTarget.y = 0f;

        Vector3 cameraForward = vrCamera.forward;
        cameraForward.y = 0f;

        float signedAngle = Vector3.SignedAngle(
            cameraForward,
            directionToTarget,
            Vector3.up
        );

        if (Mathf.Abs(signedAngle) <= visibleAngleThreshold)
        {
            Hide();
            return;
        }

        bool showLeft = signedAngle < 0f;
        bool showRight = signedAngle > 0f;

        leftArrow.SetActive(showLeft);
        rightArrow.SetActive(showRight);

        if (guideText != null)
        {
            guideText.text = showLeft
                ? "Turn Left"
                : "Turn Right";
        }
    }

    private void Hide()
    {
        if (leftArrow != null) leftArrow.SetActive(false);
        if (rightArrow != null) rightArrow.SetActive(false);

        if (guideText != null)
            guideText.text = "";
    }

    public void HideAll()
    {
        if (leftArrow != null) leftArrow.SetActive(false);
        if (rightArrow != null) rightArrow.SetActive(false);

        if (guideText != null)
            guideText.text = "";

        targetObject = null;
    }

    public void SetTarget(Transform newTarget)
    {
        if (newTarget == null) HideAll();
        targetObject = newTarget;
    }
}