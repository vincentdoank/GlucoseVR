using UnityEngine;

public class CanvasFollowTarget : MonoBehaviour
{
    public enum OffsetSpace
    {
        TargetFacing,
        CameraFacing
    }

    [Header("Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private bool followTarget = true;
    [SerializeField] private bool followCameraRotation = false;
    [SerializeField] private float smoothness = 10f;

    [Header("Offset Mode")]
    [SerializeField] private OffsetSpace offsetSpace = OffsetSpace.CameraFacing;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        UpdateCanvasTransform(true);
    }

    private void LateUpdate()
    {
        UpdateCanvasTransform(false);
    }

    private void UpdateCanvasTransform(bool instant)
    {
        if (mainCamera == null || target == null)
            return;

        Vector3 lookDirection =
            target.position - mainCamera.transform.position;

        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude < 0.001f)
            return;

        Quaternion facingRotation =
            Quaternion.LookRotation(lookDirection, Vector3.up);

        Vector3 offset = GetOffset(facingRotation);

        Vector3 targetPosition =
            followTarget
                ? target.position + offset
                : transform.position;

        if (instant || smoothness <= 0f)
        {
            transform.position = targetPosition;

            if (followCameraRotation)
            {
                transform.rotation = facingRotation;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                Time.deltaTime * smoothness
            );

            if (followCameraRotation)
            {
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    facingRotation,
                    Time.deltaTime * smoothness
                );
            }
        }
    }

    private Vector3 GetOffset(Quaternion facingRotation)
    {
        switch (offsetSpace)
        {
            case OffsetSpace.TargetFacing:
                return target.rotation * positionOffset;

            case OffsetSpace.CameraFacing:
                return facingRotation * positionOffset;

            default:
                return positionOffset;
        }
    }
}