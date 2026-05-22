using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class VRTeleportToPoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform xrOrigin;

    [Header("Teleport Target")]
    [SerializeField] private Transform teleportTarget;

    public void Teleport()
    {
        if (xrOrigin == null || teleportTarget == null)
        {
            Debug.LogWarning("XR Origin or teleport target is missing.");
            return;
        }

        xrOrigin.position = teleportTarget.position;
        xrOrigin.rotation = teleportTarget.rotation;
    }
}