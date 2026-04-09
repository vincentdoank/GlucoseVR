using UnityEngine;
using DG.Tweening;
using UnityEngine.XR.Interaction.Toolkit;

public class Phone : MonoBehaviour
{
    [SerializeField] private GameObject tapIconGo;
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;

    private bool isTapVisible;
    private HandTouchController grabbingHand;

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrabbed);
        grabInteractable.selectExited.RemoveListener(OnReleased);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hand") || !isTapVisible)
            return;

        //OnTouchTriggered();
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Transform handTransform = args.interactorObject.transform;

        grabbingHand = handTransform.GetComponentInParent<HandTouchController>();

        if (grabbingHand != null)
        {
            grabbingHand.SetTouchEnabled(false);
        }

        HideTapIcon();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        if (grabbingHand != null)
        {
            grabbingHand.SetTouchEnabled(true);
            grabbingHand = null;
        }
    }

    public void ShowTapIcon()
    {
        Debug.LogWarning("ShowTapIcon");
        isTapVisible = true;

        tapIconGo.SetActive(true);
        tapIconGo.transform.localScale = Vector3.zero;

        tapIconGo.transform
            .DOScale(1f, 0.2f)
            .SetEase(Ease.OutBack).Play();
    }

    public void HideTapIcon()
    {
        isTapVisible = false;

        tapIconGo.transform
            .DOScale(0f, 0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() => tapIconGo.SetActive(false)).Play();
    }

    public void OnTouchTriggered()
    {
        GameManager.Instance.ShowBluetoothIcon();
        HideTapIcon();
    }
}