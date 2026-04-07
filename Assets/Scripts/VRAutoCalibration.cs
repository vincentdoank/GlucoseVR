using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRAutoCalibration : MonoBehaviour
{
    [Header("XR References")]
    [SerializeField] private Transform xrCamera;
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;

    [Header("Avatar")]
    [SerializeField] private Transform avatarRoot;
    [SerializeField] private Animator avatarAnimator;

    [Header("Calibration")]
    [SerializeField] private float referenceAvatarHeight = 1.7f;
    [SerializeField] private bool autoCalibrateOnStart = true;
    [SerializeField] private float floorOffset = 0f;

    [Header("Optional IK Targets")]
    [SerializeField] private Transform headTarget;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform rightHandTarget;

    private float calibratedScale = 1f;

    private void Start()
    {
        if (autoCalibrateOnStart)
        {
            Invoke(nameof(Calibrate), 0.5f);
        }
    }

    public void Calibrate()
    {
        if (xrCamera == null || avatarRoot == null)
        {
            Debug.LogError("Calibration failed: Missing references.");
            return;
        }

        float playerHeight = xrCamera.localPosition.y;

        if (playerHeight <= 0.5f)
        {
            Debug.LogWarning("Player height seems invalid.");
            return;
        }

        calibratedScale = playerHeight / referenceAvatarHeight;

        avatarRoot.localScale = Vector3.one * calibratedScale;

        Vector3 avatarPos = avatarRoot.position;
        avatarPos.y = floorOffset;
        avatarRoot.position = avatarPos;

        UpdateIKTargets();

        Debug.Log($"Avatar calibrated. Height: {playerHeight:F2}m Scale: {calibratedScale:F2}");
    }

    private void UpdateIKTargets()
    {
        if (headTarget != null)
            headTarget.position = xrCamera.position;

        if (leftHandTarget != null && leftController != null)
            leftHandTarget.position = leftController.position;

        if (rightHandTarget != null && rightController != null)
            rightHandTarget.position = rightController.position;
    }

    public float GetCurrentScale()
    {
        return calibratedScale;
    }
}