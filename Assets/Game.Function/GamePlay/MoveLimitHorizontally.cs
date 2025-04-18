using UnityEngine;

public class MoveLimitHorizontally : MonoBehaviour
{
    bool dontMoveLeft, dontMoveRight;

    Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }


    public void SetNewPos(Vector2 newPos)
    {
        var oldPos = _transform.position;

        if (dontMoveLeft && newPos.x < oldPos.x) return;

        if (dontMoveRight && newPos.x > oldPos.x) return;

        _transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var tag = collision.gameObject.tag;

        print("--> " + tag);

        if (tag.Equals(ConstTagEngine.BorderLeft))
        {
            dontMoveLeft = true;
        }
        else if (tag.Equals(ConstTagEngine.BorderRight))
        {
            dontMoveRight = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(ConstTagEngine.BorderLeft))
        {
            dontMoveLeft = false;
        }
        else if (collision.gameObject.CompareTag(ConstTagEngine.BorderRight))
        {
            dontMoveRight = false;
        }
    }
}
