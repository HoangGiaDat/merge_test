using Core.Pool;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCelebrete : MonoBehaviour
{
    Transform _transform;

    float valueScale;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        valueScale = 1;

        Zoom();
    }

    void Zoom()
    {
        _transform.DOScale(Vector2.one * valueScale, 0.25f).SetEase(Ease.OutBounce).onComplete=()=>
        {
            Invoke(nameof(UnZoom), 0.2f);
        };
    }

    void UnZoom()
    {
        _transform.DOScale(Vector2.zero, 0.35f).SetEase(Ease.InSine).onComplete = () =>
        {
            //gameObject.SetActive(false);
            SmartPool.Instance.Despawn(this.gameObject);
        };
    }
}
