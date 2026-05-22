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
            Phone phone = other.GetComponentInParent<Phone>();

            if (phone == null) return;
            Debug.Log("phone : " + other.name, other.gameObject);
            if (!showHighlight)
            {
                Highlight(); 
            }
            onPhoneReachArea?.Invoke();
            phone.ShowTapIcon();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Phone")
        {
            Phone phone = other.GetComponentInParent<Phone>();

            if (phone == null) return;
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
