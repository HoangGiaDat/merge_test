using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoiCauBong : MonoBehaviour
{
    [SerializeField] bool userAnimMove = true;

    [SerializeField] bool userImg = true;

    [SerializeField] Image imgIcon;

    [SerializeField] SpriteRenderer renderIcon;

    [SerializeField] float scaleIcon = 1f;

    Transform _tramsform;

    Transform _trasformIcon;

    Sprite spriteIcon;

    float yMin, yMax;

    private void Awake()
    {
        _tramsform = transform;

        if(userImg) _trasformIcon = imgIcon.transform;
        else _trasformIcon = renderIcon.transform;
    }

    private void Start()
    {
        if(userImg)
        {
            yMin = transform.localPosition.y - 5f;
            yMax = transform.localPosition.y + 5f;
        }
        else
        {
            yMin = transform.localPosition.y - 0.1f;
            yMax = transform.localPosition.y + 0.1f;
        }

        if(userAnimMove)
        {
            _tramsform.localPosition = new Vector2(_tramsform.localPosition.x, yMin);

            MoveUp();
        }
    }

    public void ShowNewBall(Sprite _spriteIcon)
    {
        spriteIcon = _spriteIcon;

        UnZoomIcon();
    }

    void UnZoomIcon()
    {
        _trasformIcon.DOScale(Vector2.zero, 0.1f).SetEase(Ease.InSine).onComplete = () => { ZoomIcon(); };
    }

    void ZoomIcon()
    {
        if(userImg) imgIcon.sprite = spriteIcon;
        else renderIcon.sprite = spriteIcon;

        _trasformIcon.DOScale(Vector2.one * scaleIcon, 0.25f).SetEase(Ease.OutSine);
    }

    void MoveDown()
    {
        _tramsform.DOLocalMoveY(yMin, 1f).SetEase(Ease.OutSine).onComplete = () => { MoveUp(); };
    }

    void MoveUp()
    {
        _tramsform.DOLocalMoveY(yMax, 1f).SetEase(Ease.OutSine).onComplete = () => { MoveDown(); };
    }
}