using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AvatarSetup : MonoBehaviour
{

    [Header("Sitting")]
    [SerializeField] private bool allowSitting = true;
    [SerializeField] private float sittingFollowStrength = 0.8f;

    private float standingHeadY;
    private float initialRootY;

    [Header("Body Height Offset")]
    [SerializeField] private float bodyVerticalOffset = -0.15f;

    [Header("Tracking Anchors")]
    [SerializeField] private Transform headAnchor;               // Main Camera
    [SerializeField] private Transform leftHandTargetTracker;
    [SerializeField] private Transform rightHandTargetTracker;

    [Header("XR References")]
    [SerializeField] private Transform xrOrigin;
    [SerializeField] private Transform trackingSpace;            // Camera Offset

    [Header("Avatar Root")]
    [SerializeField] private Transform avatarRoot;               // Body
    [SerializeField] private Transform headBone;                // Avatar Head Bone

    [Header("Body Follow Settings")]
    [SerializeField] private float maxHeadYawBeforeTurn = 5f;
    [SerializeField] private float bodyTurnSpeed = 120f;
    [SerializeField] private bool followXZPosition = true;

    [Header("Face Alignment Offset")]
    [Tooltip("Positive Z pushes avatar slightly backward so camera sits in front of face")]
    [SerializeField] private Vector3 faceOffset = new Vector3(0f, 0f, 0.08f);

    [Header("Calibration")]
    [SerializeField] private float referenceHeight = 1.7f;
    [SerializeField] private bool autoCalibrateOnStart = true;


    private Vector3 initialHeadToRootOffset;

    private void Start()
    {
        if (avatarRoot == null || headBone == null)
        {
            Debug.LogError("Avatar Root or Head Bone is missing.");
            return;
        }

        initialHeadToRootOffset =
            avatarRoot.position - headBone.position;

        initialRootY = avatarRoot.position.y;
        standingHeadY = headAnchor.position.y;

        if (autoCalibrateOnStart)
        {
            Invoke(nameof(CalibrateHeight), 0.5f);
        }
    }

    private void LateUpdate()
    {
        if (avatarRoot == null || headAnchor == null || headBone == null)
            return;

        RotateBodyTowardsHead();
        FollowPlayerPosition();
    }

    private void FollowPlayerPosition()
    {
        if (!followXZPosition)
            return;

        Vector3 flatForward = headAnchor.forward;
        flatForward.y = 0f;

        if (flatForward.sqrMagnitude < 0.001f)
            flatForward = avatarRoot.forward;

        Quaternion yawRotation =
            Quaternion.LookRotation(flatForward, Vector3.up);

        Vector3 rotatedFaceOffset =
            yawRotation * faceOffset;

        Vector3 targetPosition =
            headAnchor.position +
            initialHeadToRootOffset -
            rotatedFaceOffset;

        if (allowSitting)
        {
            float heightDelta =
                headAnchor.position.y - standingHeadY;

            targetPosition.y =
                initialRootY +
                heightDelta * sittingFollowStrength +
                bodyVerticalOffset;
        }
        else
        {
            targetPosition.y =
                initialRootY + bodyVerticalOffset;
        }

        avatarRoot.position = targetPosition;
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
            Quaternion targetRotation =
                Quaternion.LookRotation(headForward);

            avatarRoot.rotation = Quaternion.RotateTowards(
                avatarRoot.rotation,
                targetRotation,
                bodyTurnSpeed * Time.deltaTime
            );
        }
    }

    public void CalibrateHeight()
    {
        if (trackingSpace == null)
        {
            Debug.LogWarning("Tracking space is missing.");
            return;
        }

        float playerHeight =
            headAnchor.position.y - trackingSpace.position.y;

        if (playerHeight < 0.5f)
        {
            Debug.LogWarning("Invalid headset height.");
            return;
        }

        float scale = playerHeight / referenceHeight;
        //avatarRoot.localScale = Vector3.one * scale;

        Debug.Log(
            $"Avatar calibrated. Height: {playerHeight:F2}m, Scale: {scale:F2}"
        );
    }
}