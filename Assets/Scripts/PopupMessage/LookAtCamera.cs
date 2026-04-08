using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera camera;
    public Vector3 cameraOffset;
    public float smoothness;

    private Vector3 initialPos;

    private void Awake()
    {
        camera = Camera.main;
        initialPos = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 dir = camera.transform.position - initialPos;
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position, camera.transform.up);
        transform.position = Vector3.Lerp(transform.position, initialPos + transform.right * cameraOffset.x + transform.up * cameraOffset.y + transform.forward * cameraOffset.z, Time.deltaTime * smoothness);
    }
}
