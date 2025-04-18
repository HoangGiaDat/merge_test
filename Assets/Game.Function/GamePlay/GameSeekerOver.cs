using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSeekerOver : MonoBehaviour
{
    public UnityEvent actionEndGame;

    [HideInInspector] public bool isLose;

    Ball _ball;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(ConstTagEngine.Ball))
        {
            if (isLose) return;

            _ball = collision.gameObject.GetComponent<Ball>();

            if (_ball.GetVelocityY() < 0) return;

            var vX = _ball.GetVelocityX();

            if (vX < -0.05f) return;

            if (vX > 0.05f) return;

            isLose = true;

            _ball.ChangeColorReb();

            if(actionEndGame != null) actionEndGame.Invoke();
        }
    }

    public void ResetStatusLose()
    {
        isLose = false;

        _ball.ReturnColorWhite();
    }
}
