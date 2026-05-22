using UnityEngine;

public class HandGrabAnimator : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    [Header("Parameter Names")]
    [SerializeField] private string leftGrabParam = "leftGrab";
    [SerializeField] private string rightGrabParam = "rightGrab";

    [Header("Grab States")]
    [SerializeField] private int emptyState = 0;
    [SerializeField] private int grabbingState = 1;

    /// <summary>
    /// Left hand grabs an item.
    /// itemType can be any integer representing the object type.
    /// </summary>
    public void GrabLeft(int itemType)
    {
        animator.SetFloat(leftGrabParam, itemType);
        animator.SetFloat("leftGrabState", 1);
        Debug.Log("Grab : " + leftGrabParam + " " + itemType);
    }

    /// <summary>
    /// Right hand grabs an item.
    /// </summary>
    public void GrabRight(int itemType)
    {
        animator.SetFloat(rightGrabParam, itemType);
        animator.SetFloat("rightGrabState", 1);
        Debug.Log("GrabRight : " + rightGrabParam + " " + itemType);
    }

    /// <summary>
    /// Left hand releases item.
    /// </summary>
    public void ReleaseLeft()
    {
        animator.SetFloat(leftGrabParam, emptyState);
        animator.SetFloat("leftGrabState", 0);
    }

    /// <summary>
    /// Right hand releases item.
    /// </summary>
    public void ReleaseRight()
    {
        animator.SetFloat(rightGrabParam, emptyState);
        animator.SetFloat("rightGrabState", 0);
    }

    /// <summary>
    /// Check if left hand is holding something.
    /// </summary>
    public bool IsLeftGrabbing()
    {
        return animator.GetFloat(leftGrabParam) >= grabbingState;
    }

    /// <summary>
    /// Check if right hand is holding something.
    /// </summary>
    public bool IsRightGrabbing()
    {
        return animator.GetFloat(rightGrabParam) >= grabbingState;
    }
}