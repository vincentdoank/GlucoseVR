using UnityEngine;
using UnityEngine.Events;

public class SnapArm : MonoBehaviour
{
    public GameObject snappedCuff;
    public GameObject placeholder;
    public GameObject realObject;

    public string targetTag;
    public bool hideRealObject = true;

    public UnityEvent onItemSnapped;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == targetTag)
        {
            ShowSnappedCuff();
            onItemSnapped?.Invoke();
        }
    }

    public void ShowSnappedCuff()
    {
        if (snappedCuff)
            snappedCuff.SetActive(true);
        if (placeholder)
            placeholder.SetActive(false);
        if (realObject && hideRealObject)
            realObject.SetActive(false);
    }

    public void HideSnappedCuff()
    {
        if(snappedCuff)
            snappedCuff.SetActive(false);
    }

    public void ShowPlaceholder()
    {
        if(placeholder)
            placeholder.SetActive(true);
    }
}
