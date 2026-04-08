using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public GameObject bluetoothGo;

    private Tweener bluetoothIdle;

    public static GameManager Instance { get; private set; }

    private void Start()
    {
        Instance = this;
    }

    public void ShowBluetoothIcon()
    {
        bluetoothGo.SetActive(true);
        bluetoothGo.transform.localScale = Vector3.zero;
        bluetoothGo.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack).OnComplete(() => {
            bluetoothIdle = bluetoothGo.transform.DOScale(1.1f, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        }).Play();
    }

    public void HideBluetoothIcon()
    {
        if (bluetoothIdle != null) bluetoothIdle.Kill();
        bluetoothGo.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack).OnComplete(() => {
            bluetoothGo.SetActive(false);
        }).Play();
    }
}
