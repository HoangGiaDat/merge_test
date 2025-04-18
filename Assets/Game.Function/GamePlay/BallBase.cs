using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class BallBase : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteBall;

    [SerializeField] protected bool usePolyGon;

    [HideInInspector] public bool isFall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _transform = transform;
    }

    protected float gravityDefault = 1.65f;

    protected float rbDragDefault = 0.1f;

    protected bool canCollision = true;

    protected Rigidbody2D rb;

    protected PolygonCollider2D poly;

    protected Transform _transform;

    public float scale;

    public bool ApproveScale;

    private void Update()
    {
        if (ApproveScale)
        {
            if (this.gameObject.transform.localScale.x < scale)
            {

                if (this.gameObject.transform.localScale.x >= scale)
                {
                    this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
                    ApproveScale = false;
                }
                this.gameObject.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime * 175;
            }
        }

    }


    public virtual void SetData(int _idBall)
    {
        rb.gravityScale = 0;
    }

    public virtual void ActiveRb(bool isActionMerge)
    {
        isFall = true;

        canCollision = true;

        rb.drag = rbDragDefault;

        rb.gravityScale = gravityDefault;
    }

    public virtual void EnablePhysic()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public virtual void ReturnRb()
    {
        canCollision = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
       
        ActiveRb(false);
    }

    public virtual void DeActiveRb()
    {
        canCollision = false;
        ApproveScale = true;
        isFall = false;
        gameObject.transform.localScale = Vector3.zero;
    }

    public virtual void SetPosBall(Vector2 newPos, float left, float right)
    {
        var xmin = GetLimitPosLeft(left);
        var xmax = GetLimitPosRight(right);

        if (newPos.x < xmin) newPos.x = xmin;
        if (newPos.x > xmax) newPos.x = xmax;

        _transform.position = newPos;
    }

    public virtual void Shake()
    {
        _transform.DOShakePosition(3f, 0.15f, 20, 180, false, true, ShakeRandomnessMode.Harmonic).SetEase(Ease.InSine);
    }

    protected float GetLimitPosLeft(float valueLeft)
    {
        return valueLeft + spriteBall.bounds.size.x / 2f;
    }

    protected float GetLimitPosRight(float valueRight)
    {
        return valueRight - spriteBall.bounds.size.x / 2f;
    }

}
