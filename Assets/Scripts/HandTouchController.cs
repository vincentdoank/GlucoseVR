using UnityEngine;

public class HandTouchController : MonoBehaviour
{
    [SerializeField] private Collider touchCollider;

    public void SetTouchEnabled(bool enabled)
    {
        if (touchCollider != null)
        {
            touchCollider.enabled = enabled;
        }
    }
}