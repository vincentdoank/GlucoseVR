using UnityEngine;

public class CanvasFollowCamera : MonoBehaviour
{
    public Vector3 cameraOffset;
    public bool followCamera;
    public float smoothness;

    private void Start()
    {
        Camera camera = Camera.main;
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position, Vector3.up);
        if (followCamera)
        {
            transform.position = camera.transform.position + camera.transform.forward * cameraOffset.z + camera.transform.right * cameraOffset.x + camera.transform.up * cameraOffset.y;
        }
    }

    private void LateUpdate()
    {
        Camera camera = Camera.main;
        if (smoothness > 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(transform.position - camera.transform.position, Vector3.up), Time.deltaTime * smoothness);
            if (followCamera)
            {
                transform.position = Vector3.Lerp(transform.position, camera.transform.position + camera.transform.forward * cameraOffset.z + camera.transform.right * cameraOffset.x + camera.transform.up * cameraOffset.y, Time.deltaTime * smoothness);
            }
        }
        else
        {
            transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position, Vector3.up);
            if (followCamera)
            {
                transform.position = camera.transform.position + camera.transform.forward * cameraOffset.z + camera.transform.right * cameraOffset.x + camera.transform.up * cameraOffset.y;
            }
        }
    }

    private void Update()
    {
        //if (smoothness <= 0)
        //{
        //    transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position, camera.transform.up);
        //    if (followCamera)
        //    {
        //        transform.position = camera.transform.position + camera.transform.forward * cameraOffset.z + camera.transform.right * cameraOffset.x + camera.transform.up * cameraOffset.y;
        //    }
        //}
    }
}
