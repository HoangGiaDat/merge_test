using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointExploerBall : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.gameObject.CompareTag(ConstTagEngine.DamageBall))
        {
            ClickAreaController.instance.ActionAddPointBomb(GetComponent<Ball>().idBall, transform.position);

            PoolingObjectManager.instance.Despawn(gameObject);
        }
        else if(collision.gameObject.CompareTag(ConstTagEngine.CabybaraNet))
        {
            var distance = Vector2.Distance(collision.gameObject.transform.position, transform.position);

            if (distance < 1.35f)
            {
                GetComponent<Ball>().StickNet(collision.transform.position, 1.25f);
            }
        }*/
    }
}
