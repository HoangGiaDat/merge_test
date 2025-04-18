using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSetupController : SingletonMono<BallSetupController>
{
    [SerializeField] PoolingObjectManager poolManager;

    public Transform GetBall(Vector2 posCreate, int indexBall, float scale, int layerBall)
    {
        Transform _ball = null;

        _ball = poolManager.Spawn(indexBall, posCreate).transform;

        _ball.gameObject.layer = layerBall;

        return _ball;
    }
}
