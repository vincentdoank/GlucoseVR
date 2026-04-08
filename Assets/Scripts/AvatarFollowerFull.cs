using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AvatarFollowerFull : MonoBehaviour
{
    //[Header("References")]
    //[SerializeField] private Transform headAnchor; // XR head (CenterEyeAnchor)

    //[Header("Avatar Rig Bones")]
    //[SerializeField] private Transform bodyRoot;
    //[SerializeField] private Transform headBone;
    //[SerializeField] private TwoBoneIKConstraint leftArmTwoBone;
    //[SerializeField] private TwoBoneIKConstraint rightArmTwoBone;

    //[Header("Offsets")]
    //[SerializeField] private Vector3 bodyOffset = new Vector3(0, 0, -0.15f);
    //[SerializeField] private Vector3 rotationOffset;
    //[SerializeField] private Vector3 leftHandEulerOffset;
    //[SerializeField] private Vector3 rightHandEulerOffset;
    //[SerializeField] private Vector3 leftHandPositionOffset;
    //[SerializeField] private Vector3 rightHandPositionOffset;

    //[SerializeField] private float smoothSpeed = 10f;
    //[SerializeField] private Vector2 neckOffset;
    //[SerializeField] private float targetEyeHeight = 1.6f; // uniform avatar visual height

    //[Header("Ground Settings")]
    //[SerializeField] private LayerMask groundMask;
    //[SerializeField] private float groundRayLength = 5f;

    //[Header("Body Rotation")]
    //[SerializeField] private float maxHeadYawBeforeTurn = 45f;
    //[SerializeField] private float bodyTurnSpeed = 120f;

    //[Header("Local Settings")]
    //[SerializeField] private bool hideHeadForLocal = true;
    //[SerializeField] private SkinnedMeshRenderer[] headRenderers;

    //[SerializeField] private AvatarSetup avatarSetup;

    //private Transform leftHandTracker;
    //private Transform rightHandTracker;
    //private float currentBodyYaw;
    //private bool isLocal;

    //private MetaMovementAnimator metaMovementAnimator;

    //// Smooth buffers
    //private Vector3 smoothedHeadPos, smoothedLeftPos, smoothedRightPos;
    //private Quaternion smoothedHeadRot, smoothedLeftRot, smoothedRightRot;

    //private void Start()
    //{
    //    InitLocal();
    //}

    //public void InitLocal()
    //{
    //    StartCoroutine(WaitForSpawn());
    //    if (avatarSetup)
    //    {
    //        headAnchor = avatarSetup.headAnchor;
    //        leftHandTracker = avatarSetup.leftHandTargetTracker;
    //        rightHandTracker = avatarSetup.rightHandTargetTracker;

    //    }

    //    metaMovementAnimator = GetComponent<MetaMovementAnimator>();

    //    if (bodyRoot)
    //        currentBodyYaw = bodyRoot.eulerAngles.y;
    //}

    //private IEnumerator WaitForSpawn()
    //{
    //    yield return new WaitForSeconds(2f);
    //}

    //public void OnLeftGrabbed(float grabbingState, float grab)
    //{
    //    // Add logic like holding animation or sync
    //    metaMovementAnimator.OnLeftGrab(grabbingState, grab);
    //}

    //public void OnRightGrabbed(float grabbingState, float grab)
    //{
    //    // Add logic like holding animation or sync
    //    metaMovementAnimator.OnRightGrab(grabbingState, grab);
    //}

    //public void OnLeftReleased(float grab)
    //{
    //    // Add release logic here
    //    Debug.LogWarning("Left Release");
    //    metaMovementAnimator.OnLeftGrab(0, grab);
    //}
    //public void OnRightReleased(float grab)
    //{
    //    // Add release logic here
    //    Debug.LogWarning("Right Release");
    //    metaMovementAnimator.OnRightGrab(0, grab);
    //}

    //private void LateUpdate()
    //{
    //    if (!bodyRoot) return;

    //    UpdateLocalAvatar();
    //}

    //private void UpdateLocalAvatar()
    //{
    //    if (!headAnchor || !bodyRoot) return;

    //    // --- Flattened forward for slight body offset behind head ---
    //    Vector3 flatForward = headAnchor.forward;
    //    flatForward.y = 0f;
    //    flatForward.Normalize();

    //    // Move the body so it's slightly behind the head and matches head height
    //    Vector3 targetBodyPos = headAnchor.position + flatForward * neckOffset.x + Vector3.down * neckOffset.y;

    //    // Smooth movement for stability
    //    bodyRoot.position = targetBodyPos;

    //    // --- Body rotation follows headset yaw ---
    //    float headYaw = headAnchor.eulerAngles.y;
    //    float yawDiff = Mathf.DeltaAngle(currentBodyYaw, headYaw);
    //    if (Mathf.Abs(yawDiff) > maxHeadYawBeforeTurn)
    //        currentBodyYaw = Mathf.MoveTowardsAngle(currentBodyYaw, headYaw, bodyTurnSpeed * Time.deltaTime);

    //    bodyRoot.rotation = Quaternion.Euler(0, currentBodyYaw, 0);

    //    // --- HeadBone follows headset exactly ---
    //    if (headBone)
    //    {
    //        headBone.position = headAnchor.position;
    //        headBone.rotation = headAnchor.rotation;
    //    }

    //    // --- Apply hand IK ---
    //    ApplyHandIK(leftArmTwoBone, leftHandTracker, leftHandPositionOffset, leftHandEulerOffset);
    //    ApplyHandIK(rightArmTwoBone, rightHandTracker, rightHandPositionOffset, rightHandEulerOffset);
    //}


    //private void UpdateAvatar(Vector3 headPos, Quaternion headRot,
    //                       Vector3 leftHandPos, Quaternion leftHandRot,
    //                       Vector3 rightHandPos, Quaternion rightHandRot)
    //{
    //    // --- Flattened forward for same body offset as local ---
    //    Vector3 flatForward = headRot * Vector3.forward;
    //    flatForward.y = 0f;
    //    flatForward.Normalize();

    //    // Body follows head position and rotation (floating, not grounded)
    //    Vector3 targetBodyPos = headPos + flatForward * neckOffset.x + Vector3.down * neckOffset.y;
    //    bodyRoot.position = targetBodyPos;
    //    bodyRoot.rotation = Quaternion.Euler(0, headRot.eulerAngles.y, 0);

    //    // --- Head follows exactly the tracked position ---
    //    if (headBone)
    //    {
    //        headBone.position = headPos;
    //        headBone.rotation = headRot;
    //    }

    //    // --- Apply hand IK (mirrors local) ---
    //    ApplyHandIK(leftArmTwoBone, leftHandPos, leftHandRot, leftHandPositionOffset, leftHandEulerOffset);
    //    ApplyHandIK(rightArmTwoBone, rightHandPos, rightHandRot, rightHandPositionOffset, rightHandEulerOffset);
    //}


    //private void ApplyHandIK(TwoBoneIKConstraint ik, Transform tracker, Vector3 posOffset, Vector3 rotOffset)
    //{
    //    //if (!ik || !tracker) return;
    //    //ik.weight = 1f;
    //    //ik.data.target.position = tracker.position + posOffset;
    //    //ik.data.target.rotation = tracker.rotation * Quaternion.Euler(rotOffset);

    //    if (!ik || !tracker) return;

    //    ik.weight = 1f;

    //    // --- POSITION: apply offset in controller local space ---
    //    Vector3 worldPos = tracker.TransformPoint(posOffset);
    //    //ik.data.target.position = Vector3.Lerp(ik.data.target.position, worldPos, Time.deltaTime * smoothSpeed);
    //    ik.data.target.position = worldPos;

    //    // --- ROTATION: apply offset in controller local space ---
    //    Quaternion worldRot = tracker.rotation * Quaternion.Euler(rotOffset);
    //    //ik.data.target.rotation = Quaternion.Lerp(ik.data.target.rotation, worldRot, Time.deltaTime * smoothSpeed);
    //    ik.data.target.rotation = worldRot;
    //}

    //private void ApplyHandIK(TwoBoneIKConstraint ik, Vector3 pos, Quaternion rot, Vector3 posOffset, Vector3 rotOffset)
    //{
    //    //if (!ik) return;
    //    //ik.weight = 1f;
    //    //ik.data.target.position = pos + posOffset;
    //    //ik.data.target.rotation = rot * Quaternion.Euler(rotOffset);

    //    if (!ik) return;

    //    ik.weight = 1f;

    //    // --- POSITION: convert offset into controller local space ---
    //    Vector3 worldPos = pos + rot * posOffset;
    //    ik.data.target.position = worldPos;

    //    // --- ROTATION: apply rotOffset in controller local space ---
    //    Quaternion worldRot = rot * Quaternion.Euler(rotOffset);
    //    ik.data.target.rotation = worldRot;
    //}
}
