using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator animator;

    private static readonly int WavingHash = Animator.StringToHash("waving");
    private static readonly int IdleHash = Animator.StringToHash("idle");
    private static readonly int SitHash = Animator.StringToHash("sit");
    private static readonly int HappyHash = Animator.StringToHash("happy");
    private static readonly int FlipHandHash = Animator.StringToHash("flipHand");

    public void PlayWaving()
    {
        animator.SetTrigger(WavingHash);
    }

    public void PlayIdle()
    {
        animator.SetTrigger(IdleHash);
    }

    public void PlaySit()
    {
        animator.SetTrigger(SitHash);
    }

    public void PlayHappy()
    {
        animator.SetTrigger(HappyHash);
    }

    public void PlayFlipHand()
    {
        animator.SetTrigger(FlipHandHash);
    }
}   