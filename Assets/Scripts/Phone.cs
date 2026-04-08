using UnityEngine;
using DG.Tweening;

public class Phone : MonoBehaviour
{
    public GameObject tapIconGo;

    public void ShowTapIcon()
    {
        tapIconGo.SetActive(true);
    }

    public void HideTapIcon()
    {
        tapIconGo.transform.DOScale(0, 0.2f).SetEase(Ease.InBack).Play();
    }
}
