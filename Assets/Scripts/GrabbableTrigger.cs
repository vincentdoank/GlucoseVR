using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
public class GrabbableTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandGrabAnimator handAnimator;

    [Header("Item Settings")]
    [SerializeField] private int itemType = 1;

    private XRGrabInteractable grabInteractable;

    public XRInteractionManager interactionManager;

    private Quaternion defaultRot;
    private Vector3 defaultPos;

    private void Start()
    {
        defaultRot = transform.rotation;
        defaultPos = transform.position;
    }

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        var interactorObject = args.interactorObject.transform;
        Debug.LogWarning("tag : " + interactorObject.tag, interactorObject);
        if (interactorObject.CompareTag("LeftHand"))
        {
            handAnimator.GrabLeft(itemType);
        }
        else if (interactorObject.CompareTag("RightHand"))
        {
            handAnimator.GrabRight(itemType);
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        var interactorObject = args.interactorObject.transform;

        if (interactorObject.CompareTag("LeftHand"))
        {
            handAnimator.ReleaseLeft();
        }
        else if (interactorObject.CompareTag("RightHand"))
        {
            handAnimator.ReleaseRight();
        }
    }

    public void ForceRelease()
    {
        if (grabInteractable == null) return;

        if (grabInteractable.isSelected)
        {
            var interactor = grabInteractable.firstInteractorSelecting;
            interactionManager.SelectExit(interactor, grabInteractable);
        }
    }

    public void ResetPosition()
    {
        transform.rotation = defaultRot;
        transform.position = defaultPos;
    }
}