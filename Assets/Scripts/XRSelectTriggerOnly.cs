using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class XRSelectTriggerOnly : XRGrabInteractable
{
    [Header("Events")]
    [SerializeField] private UnityEvent onSelected;

    private bool canGrab;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (canGrab)
        {
            // Call base so XR interaction events still work
            base.OnSelectEntered(args);

            // Trigger your custom event

            onSelected?.Invoke();

            // Immediately release so it doesn't stay grabbed
            interactionManager.SelectExit(args.interactorObject, this);
        }
    }

    public void SetCanGrab(bool canGrab)
    {
        this.canGrab = canGrab;
    }
}