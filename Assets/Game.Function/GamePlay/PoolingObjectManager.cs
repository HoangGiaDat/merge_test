
using Core.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolingObjectManager : MonoBehaviour
{
    public static PoolingObjectManager instance = null;

    public GameObject[] prefabBalls;

    [SerializeField] FactoryFXGenerator poolFX;

    List<Transform> parentBall = new List<Transform>();

    List<BallLevel> collectionBalls = new List<BallLevel>();

    [SerializeField] Transform parentBomb;

    int[] listPoints = new int[] { 1, 3, 6, 10, 15, 25, 50, 90, 125, 175, 300 };

    int countSpawnBall;

    bool firstTimeChangeEmotionBall = true;

    public BallSizeDataBase dataBall;

    public void Awake()
    {
        instance = this;

        CreatePool(10);
    }

    public void CreatePool(int preLoad)
    {
        for (int j = 0; j < prefabBalls.Length; j++)
        {
            GameObject newParentBall = new GameObject("Pool " + prefabBalls[j].name);

            newParentBall.transform.parent = transform;

            parentBall.Add(newParentBall.transform);

            collectionBalls.Add(new BallLevel());
            
        }
    }

    public GameObject Spawn(int idBall, Vector2 posCreate)
    {
        GameObject obj = null;

        var listBall = collectionBalls[idBall].balls;

        for (int i = 0; i < listBall.Count; i++)
        {
            if (listBall[i] != null && !listBall[i].activeInHierarchy)
            {
                obj = listBall[i];
            }
        }

        if (obj == null)
        {
            GameObject newObj = SmartPool.Instance.Spawn(prefabBalls[idBall], parentBall[idBall].position, Quaternion.identity);

            newObj.transform.parent = parentBall[idBall];

            collectionBalls[idBall].balls.Add(newObj);

            var sizer = dataBall.size1[idBall];
            var ball = newObj.GetComponent<Ball>();
            ball.gameObject.transform.localScale = Vector3.zero;
            ball.DeActiveRb();
            ball.idBall = idBall;

            // ball.gameObject.transform.localScale = new Vector3(sizer, sizer, sizer);
            ball.scale = sizer;

            Debug.Log("BBBBBBBBBB");


            obj = newObj;
        }

        obj.transform.position = posCreate;

        obj.SetActive(true);

        countSpawnBall++;

        if(countSpawnBall >= 19 && firstTimeChangeEmotionBall)
        {
            firstTimeChangeEmotionBall = false;

            ChangeEmotionRandom();
        }

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.GetComponent<Ball>().DeActiveRb();

        obj.transform.rotation = Quaternion.identity;

        obj.SetActive(false);
    }

    public bool CheckShowPopupContinuePlay()
    {
        bool value = false;

        int countBallSmall = 0;

        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                var _ball = collectionBalls[i].balls[j].GetComponent<Ball>();

                if (_ball.gameObject.activeInHierarchy && _ball.idBall < 3) countBallSmall++;
            }
        }

        if (countBallSmall > 0) value = true;

        return value;
    }

    public void ShakeAllBall()
    {
        
        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                var _ball = collectionBalls[i].balls[j].GetComponent<BallBase>();

                _ball.DeActiveRb();

                _ball.Shake();
            }
        }
    }

    public void ShakeBallPlayer()
    {
        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                if(collectionBalls[i].balls[j].activeInHierarchy)
                {
                    var _ball = collectionBalls[i].balls[j].GetComponent<Ball>();

                    if (!_ball.CheckIsBallBot())
                    {
                        _ball.DeActiveRb();

                        _ball.Shake();
                    }
                }
            }
        }
    }

    public void ShakeBallBot()
    {
        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                if (collectionBalls[i].balls[j].activeInHierarchy)
                {
                    var _ball = collectionBalls[i].balls[j].GetComponent<Ball>();

                    if (_ball.CheckIsBallBot())
                    {
                        _ball.DeActiveRb();

                        _ball.Shake();
                    }
                }
            }
        }
    }

    void ChangeEmotionRandom()
    {
        List<GameObject> ballActive = new List<GameObject>();

        for (int i = 3; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                if (collectionBalls[i].balls[j].activeInHierarchy && collectionBalls[i].balls[j] != null) ballActive.Add(collectionBalls[i].balls[j]);
            }
        }

        if(ballActive.Count > 7)
        {
            int limitBallSelect = RandomInt(3, 6);

            int countSelect = 0;

            while (countSelect <= limitBallSelect)
            {
                countSelect++;

                ballActive[RandomInt(0, ballActive.Count)].GetComponent<Ball>().ChangeEmotion();
            }
        }

        Invoke(nameof(ChangeEmotionRandom), RandomInt(5, 13));
    }

    int RandomInt(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public void ChangeEmotionAllBall(int indexEmotion)
    {
        if (indexEmotion == 1) CancelInvoke(nameof(ChangeEmotionRandom));
        else ChangeEmotionRandom();

        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                var _ball = collectionBalls[i].balls[j].GetComponent<Ball>();

                if (_ball.gameObject.activeInHierarchy) _ball.SetEmotionDead(indexEmotion);
            }
        }
    }

    public void RandomDestroy()
    {
        StartCoroutine(DelayRandomDestroy());
    }

    IEnumerator DelayRandomDestroy()
    {
        List<GameObject> listObjSelect = new List<GameObject>();

        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                if (collectionBalls[i].balls[j].activeInHierarchy && collectionBalls[i].balls[j].GetComponent<Ball>().isFall)
                {
                    listObjSelect.Add(collectionBalls[i].balls[j]);
                }
            }
        }

        var limitBall = listObjSelect.Count < 5 ? listObjSelect.Count : 5;

        if (limitBall > 0)
        {
            List<int> indexRandoms = new List<int>();

            while (indexRandoms.Count < limitBall)
            {
                var index = RandomInt(0, listObjSelect.Count);

                if (!indexRandoms.Contains(index)) indexRandoms.Add(index);
            }

            for (int i = 0; i < limitBall; i++)
            {
                listObjSelect[indexRandoms[i]].GetComponent<Ball>().ChangeColorReb();
            }

            yield return new WaitForSeconds(1f);

            for (int i = 0; i < limitBall; i++)
            {
                yield return new WaitForSeconds(0.15f);

                this.PostEvent(EventID.UpdatePoint, listPoints[listObjSelect[indexRandoms[i]].GetComponent<Ball>().idBall]);

                poolFX.SpawnFx(listObjSelect[indexRandoms[i]].transform.position, listObjSelect[indexRandoms[i]].transform.localScale.x);

                listObjSelect[indexRandoms[i]].GetComponent<Ball>().ReturnColorWhite();

                Despawn(listObjSelect[indexRandoms[i]]);
            }
        }
    }

    public void SetStatusClickBall(bool status)
    {
        ClickAreaController.instance.SetStatusActive(!status);
    }
    

    public void SaveGameState()
    {
        
    }

    public int CountAllBall()
    {
        int count = 0;

        for (int i = 0; i < collectionBalls.Count; i++)
        {
            for (int j = 0; j < collectionBalls[i].balls.Count; j++)
            {
                var _ball = collectionBalls[i].balls[j].GetComponent<Ball>();

                if (_ball != null && _ball.isFall && collectionBalls[i].balls[j].activeInHierarchy)
                {
                    count++;
                }
            }
        }

        return count;
    }
}

[Serializable]
public class BallLevel
{
    public List<GameObject> balls = new List<GameObject>(); 
}
