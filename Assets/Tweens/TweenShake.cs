using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenShake : Tweening
{
    private Vector3 from;
    public float strength;
    public int vibrato;
   
    private void Awake()
    {
        from = transform.position;
    }

    public override void Play()
    {
        //transform.position = from;
        tween = ((RectTransform)transform).DOShakeAnchorPos(duration, strength, vibrato);
        Init();
        tween.Play();
    }
}
