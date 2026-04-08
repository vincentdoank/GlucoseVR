using UnityEngine;

public class Divilab : MonoBehaviour
{
    private bool showHighlight = false;
    public Custom.Outline.Outline[] outlines;

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
