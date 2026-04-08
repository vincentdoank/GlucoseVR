using UnityEngine;

public class SnapArm : MonoBehaviour
{
    public GameObject snappedCuff;
    public GameObject placeholder;
    public GameObject realObject;

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
        placeholder.SetActive(false);
        realObject.SetActive(false);
    }

    public void HideSnappedCuff()
    {
        snappedCuff.SetActive(false);
    }

    public void ShowPlaceholder()
    {
        placeholder.SetActive(true);
    }
}
