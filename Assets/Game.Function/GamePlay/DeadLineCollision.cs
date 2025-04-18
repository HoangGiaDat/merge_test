using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DeadLineCollision : MonoBehaviour
{
    [SerializeField] bool isModeBase = true;

    [SerializeField] Image imgNoti;

    bool touchBall;

    private void OnTriggerStay2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag(ConstTagEngine.Ball))
        {
            if (touchBall) return;

            var _ball = collision.gameObject.GetComponent<Ball>();

            if (_ball.GetVelocityY() < 0) return;

            var vX = _ball.GetVelocityX();

            if (vX < -0.05f) return;

            if (vX > 0.05f) return;

            touchBall = true;

            FadeNotiUp();

            if (isModeBase)
            {
                CancelInvoke(nameof(DelayReturnEmotionAllBall));  

                PoolingObjectManager.instance.ChangeEmotionAllBall((int)TypeEmotionBall.scared);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstTagEngine.Ball))
        {
            imgNoti.DOKill();

            imgNoti.color = new Color(imgNoti.color.r, imgNoti.color.g, imgNoti.color.b, 0);

            if (isModeBase && touchBall) Invoke(nameof(DelayReturnEmotionAllBall), 1f);

            touchBall = false;
        }
    }

    void DelayReturnEmotionAllBall()
    {
        PoolingObjectManager.instance.ChangeEmotionAllBall((int)TypeEmotionBall.scared);
    }

    void FadeNotiUp()
    {
        imgNoti.DOFade(1f, 0.65f).SetEase(Ease.InSine).onComplete = () => { FadeNotiDown(); };
    }

    void FadeNotiDown()
    {
        imgNoti.DOFade(0f, 0.65f).SetEase(Ease.OutSine).onComplete = () => { FadeNotiUp(); };
    }

}
