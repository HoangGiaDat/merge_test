using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;


public enum TypeEmotionBall { normal, scared, fun}

public class Ball : BallBase
{
    public int idBall;

    [SerializeField] Sprite[] spriteEmotions;

    bool isMainBall;

    bool canChangeSprite = true;


    private void OnEnable()
    {
        canCollision = false;

        SetSpriteBall((int)TypeEmotionBall.normal);
    }

    public override void SetData(int _idBall)
    {
        base.SetData(_idBall);

        idBall = _idBall;
    }

    public override void ActiveRb(bool isActionMerge)
    {
        base.ActiveRb(isActionMerge);

        isMainBall = true;

        SetSpriteBall((int)TypeEmotionBall.scared);
    }

    private IEnumerator MoveTowardsEachOther(GameObject obj1, GameObject obj2, Vector3 targetPosition, float duration, Action onComplete)
    {
        float timeElapsed = 0f;

        // Lưu vị trí ban đầu của hai object
        Vector3 startPos1 = obj1.transform.position;
        Vector3 startPos2 = obj2.transform.position;

        while (timeElapsed < duration)
        {
            // Tính toán tỷ lệ dựa trên thời gian
            float t = timeElapsed / duration;

            // Di chuyển object theo tỷ lệ
            obj1.transform.position = Vector3.Lerp(startPos1, targetPosition, t);
            obj2.transform.position = Vector3.Lerp(startPos2, targetPosition, t);

            // Tăng thời gian đã trôi qua
            timeElapsed += Time.deltaTime * 20f;

            yield return null;
        }

        // Đảm bảo vị trí cuối cùng được đặt chính xác
        obj1.transform.position = targetPosition;
        obj2.transform.position = targetPosition;

        // Thực hiện callback
        onComplete?.Invoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!canCollision) return;

        var tag = collision.gameObject.tag;


        if (tag.Equals(ConstTagEngine.Ball) || tag.Equals(ConstTagEngine.BorderBottom))
        {
            if (isMainBall)
            {
                SetSpriteBall((int)TypeEmotionBall.fun);

                isMainBall = false;
            }
        }

        if(tag.Equals(ConstTagEngine.Ball) && collision.gameObject.GetComponent<Ball>() != null)
        {
            if (!isFall) return;

            var collisionBall = collision.gameObject.GetComponent<Ball>();

            if (collisionBall.idBall >= 10) return;

            if (idBall != collisionBall.idBall) return;

            if (!collisionBall.canCollision) return;

            canCollision = false;

            var posCreate = collision.GetContact(0).point;
            Vector3 centerPosition = (transform.position + collisionBall.transform.position) / 2;
            PoolingObjectManager.instance.Despawn(collisionBall.gameObject);

            //// Bắt đầu Coroutine di chuyển
            StartCoroutine(MoveTowardsEachOther(gameObject, collisionBall.gameObject, centerPosition, 1.2f, () =>
            {
                // Sau khi di chuyển xong, Despawn và Merge
                if (!CheckIsBallBot())
                    ClickAreaController.instance.MergeBall(posCreate, idBall);

                PoolingObjectManager.instance.Despawn(gameObject);

            }));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(ConstTagEngine.Ball))
        {
            rb.drag = 0.5f;
        }
    }

    public void ZoomMerge(float valueScale)
    {
        
    }

    public float GetVelocityY()
    {
        float velocityY = 0;

        velocityY = rb.velocity.y;

        if (!isFall) velocityY = -1;

        return velocityY;
    }

    public float GetVelocityX()
    {
        return rb.velocity.x;
    }

    public void ChangeColorReb()
    {
        spriteBall.color = Color.red;

        FadeColorDown();
    }

    public void ReturnColorWhite()
    {
        spriteBall.DOKill();

        spriteBall.color = Color.white;
    }

    void FadeColorUp()
    {
        spriteBall.DOFade(1f, 0.35f).SetEase(Ease.InSine).onComplete = () => { FadeColorDown(); };
    }

    void FadeColorDown()
    {
        spriteBall.DOFade(0.5f, 0.35f).SetEase(Ease.OutSine).onComplete = () => { FadeColorUp(); };
    }

    public void SetEmotionDead(int indexType)
    {
        canChangeSprite = indexType == 2;

        spriteBall.sprite = spriteEmotions[indexType];
    }

    void SetSpriteBall(int indexType)
    {
        if (!canChangeSprite) return;

        spriteBall.sprite = spriteEmotions[indexType];
    }

    void ReturnEmotion()
    {
        SetSpriteBall((int)TypeEmotionBall.fun);
    }

    public void ChangeEmotion()
    {
        Invoke(nameof(DelayChangeEmotion), UnityEngine.Random.Range(0.1f, 1f));
    }

    void DelayChangeEmotion()
    {
        SetSpriteBall((int)TypeEmotionBall.normal);

        CancelInvoke(nameof(ReturnEmotion));

        Invoke(nameof(ReturnEmotion), UnityEngine.Random.Range(1.1f, 2.75f));
    }

    public bool CheckIsBallBot()
    {
        return gameObject.layer == 8 ? false : true;
    }
    
}
