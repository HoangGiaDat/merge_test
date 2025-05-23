using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitFall : MonoBehaviour
{
    [SerializeField] PoolingObjectManager poolManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ConstTagEngine.Ball))
        {
            poolManager.Despawn(collision.gameObject);
        }
    }
}
