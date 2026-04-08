using UnityEngine;

public class VRHandIKTargetFollower : MonoBehaviour
{
    [Header("Controller Trackers")]
    [SerializeField] private Transform leftController;
    [SerializeField] private Transform rightController;

    [Header("IK Targets")]
    [SerializeField] private Transform leftHandIKTarget;
    [SerializeField] private Transform rightHandIKTarget;

    [Header("Local Offsets")]
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

        Vector3 worldOffset =
            leftController.rotation * leftPositionOffset;

        leftHandIKTarget.position =
            leftController.position + worldOffset;

        leftHandIKTarget.rotation =
            leftController.rotation *
            Quaternion.Euler(leftRotationOffset);
    }

    private void FollowRightHand()
    {
        if (rightController == null || rightHandIKTarget == null)
            return;

        Vector3 worldOffset =
            rightController.rotation * rightPositionOffset;

        rightHandIKTarget.position =
            rightController.position + worldOffset;

        rightHandIKTarget.rotation =
            rightController.rotation *
            Quaternion.Euler(rightRotationOffset);
    }
}