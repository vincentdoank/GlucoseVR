using UnityEngine;

public class VRHandIKTargetFollower : MonoBehaviour
{
    [Header("Controller Trackers")]
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;

    [Header("IK Targets")]
    [SerializeField] private Transform leftHandIKTarget;
    [SerializeField] private Transform rightHandIKTarget;

    [Header("Offsets")]
    [SerializeField] private Vector3 leftPositionOffset;
    [SerializeField] private Vector3 rightPositionOffset;

    [SerializeField] private Vector3 leftRotationOffset;
    [SerializeField] private Vector3 rightRotationOffset;

    private void LateUpdate()
    {
        FollowLeftHand();
        FollowRightHand();
    }

    private void FollowLeftHand()
    {
        if (leftController == null || leftHandIKTarget == null)
            return;

        leftHandIKTarget.position =
            leftController.position + leftPositionOffset;

        leftHandIKTarget.rotation =
            leftController.rotation *
            Quaternion.Euler(leftRotationOffset);
    }

    private void FollowRightHand()
    {
        if (rightController == null || rightHandIKTarget == null)
            return;

        rightHandIKTarget.position =
            rightController.position + rightPositionOffset;

        rightHandIKTarget.rotation =
            rightController.rotation *
            Quaternion.Euler(rightRotationOffset);
    }
}