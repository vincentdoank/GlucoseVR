using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AvatarSetup : MonoBehaviour
{
    [Header("Tracking Anchors")]
    [SerializeField] private Transform headAnchor;
    [SerializeField] private Transform leftHandTargetTracker;
    [SerializeField] private Transform rightHandTargetTracker;

    [Header("XR References")]
    [SerializeField] private Transform xrOrigin;
    [SerializeField] private Transform trackingSpace;

    [Header("Avatar Root")]
    [SerializeField] private Transform avatarRoot;
    [SerializeField] private Transform headBone;

    [Header("Body Follow Settings")]
    [SerializeField] private float maxHeadYawBeforeTurn = 5f;
    [SerializeField] private float bodyTurnSpeed = 120f;
    [SerializeField] private bool followXZPosition = true;

    [Header("Head Follow Offset")]
    [SerializeField] private Vector3 headPositionOffset = Vector3.zero;
    [SerializeField] private Vector3 headRotationOffset = Vector3.zero;

    [Header("Calibration")]
    [SerializeField] private float referenceHeight = 1.7f;
    [SerializeField] private bool autoCalibrateOnStart = true;

    private void Start()
    {
        if (autoCalibrateOnStart)
        {
            Invoke(nameof(CalibrateHeight), 0.5f);
        }
    }

    private void LateUpdate()
    {
        if (avatarRoot == null || headAnchor == null)
            return;

        FollowPlayerPosition();
        RotateBodyTowardsHead();
        FollowHeadBone();
    }

    private void FollowPlayerPosition()
    {
        if (!followXZPosition)
            return;

        Vector3 targetPos = headAnchor.position;
        targetPos.y = avatarRoot.position.y;

        avatarRoot.position = targetPos;
    }

    private void RotateBodyTowardsHead()
    {
        Vector3 headForward = headAnchor.forward;
        headForward.y = 0f;

        if (headForward.sqrMagnitude < 0.001f)
            return;

        float angle = Vector3.SignedAngle(
            avatarRoot.forward,
            headForward,
            Vector3.up
        );

        if (Mathf.Abs(angle) > maxHeadYawBeforeTurn)
        {
            Quaternion targetRotation = Quaternion.LookRotation(headForward);

            avatarRoot.rotation = Quaternion.RotateTowards(
                avatarRoot.rotation,
                targetRotation,
                bodyTurnSpeed * Time.deltaTime
            );
        }
    }

    private void FollowHeadBone()
    {
        if (headBone == null)
            return;

        headBone.position = headAnchor.position + headPositionOffset;

        headBone.rotation =
            headAnchor.rotation *
            Quaternion.Euler(headRotationOffset);
    }

    public void CalibrateHeight()
    {
        float playerHeight =
            headAnchor.position.y - trackingSpace.position.y;

        if (playerHeight < 0.5f)
        {
            Debug.LogWarning("Invalid headset height.");
            return;
        }

        float scale = playerHeight / referenceHeight;
        avatarRoot.localScale = Vector3.one * scale;

        Debug.Log(
            $"Avatar calibrated. Height: {playerHeight:F2}, Scale: {scale:F2}"
        );
    }
}