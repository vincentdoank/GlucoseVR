using UnityEngine;
using UnityEngine.Events;

public class Divilab : MonoBehaviour
{
    private bool showHighlight = false;
    public Custom.Outline.Outline[] outlines;

    public UnityEvent onPhoneReachArea;
    public UnityEvent onPhoneLeaveArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Phone")
        {
            if (!showHighlight)
            {
                Highlight();
                Phone phone = other.GetComponent<Phone>();
                phone.ShowTapIcon();
            }
            onPhoneReachArea?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Phone")
        {
            onPhoneLeaveArea?.Invoke();
        }
    }

    public void Highlight()
    {
        showHighlight = true;
        foreach (Custom.Outline.Outline outline in outlines)
        {
            outline.enabled = true;
        }
    }

    public void HideHighlight()
    {
        foreach (Custom.Outline.Outline outline in outlines)
        {
            outline.enabled = false;
        }
    }
}
