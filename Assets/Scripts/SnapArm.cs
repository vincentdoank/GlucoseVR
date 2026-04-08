using UnityEngine;

public class SnapArm : MonoBehaviour
{
    public GameObject snappedCuff;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PressureCuff")
        {
            ShowSnappedCuff();
        }
    }

    public void ShowSnappedCuff()
    {
        snappedCuff.SetActive(true);
    }

    public void HideSnappedCuff()
    {
        snappedCuff.SetActive(false);
    }
}
