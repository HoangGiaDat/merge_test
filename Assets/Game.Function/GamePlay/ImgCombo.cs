using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImgCombo : MonoBehaviour
{
    Transform _transform;

    SpriteRenderer sprite;

    Vector2 posS;

    Transform posEnd;

    float timeMove;

    private void Awake()
    {
        _transform = transform;

        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();

        posS = transform.position;
    }

    public void UnZoom(float timeUnzoom)
    {
        transform.localScale = Vector2.one * 1.3f;

        gameObject.SetActive(true);

        _transform.DOScale(Vector2.one, timeUnzoom).SetEase(Ease.InBack).onComplete = () =>
        {
            Fade();
        };
    }

    public void Zoom(float timeZoom, float _timeMove, Transform _posEnd)
    {
        posEnd = _posEnd;

        timeMove = _timeMove;

        transform.localScale = Vector2.zero;

        gameObject.SetActive(true);

        _transform.DOScale(Vector2.one, timeZoom).SetEase(Ease.InSine).onComplete = () =>
        {
            //Fade();
        };

        Movement();

        //Invoke(nameof(Movement), timeZoom / 2);
    }

    void Movement()
    {
        _transform.DOMoveY(posEnd.position.y, timeMove).SetEase(Ease.InSine);

        Invoke(nameof(Fade), timeMove/2);
    }

    void Fade()
    {
        sprite.DOFade(0, timeMove / 2).SetEase(Ease.Linear).onComplete = () =>
        {
            ActionOff();
        };
    }

    public void ActionOff()
    {
        transform.DOKill();

        transform.position = posS;

        gameObject.SetActive(false);

        sprite.DOFade(1, 0.01f);
    }
}
