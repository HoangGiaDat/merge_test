using Core.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolFxPoint : MonoBehaviour
{
    public static PoolFxPoint instance = null;

    public GameObject[] prefabBalls;

    [SerializeField] List<Transform> parentFxPoint = new List<Transform>();

    [SerializeField] List<RallyFxPoint> collectionFxPoint = new List<RallyFxPoint>();

    private void Awake()
    {
        instance = this;

       // CreatePool(10);
    }

    //public void CreatePool(int preLoad)
    //{
    //    for (int j = 0; j < prefabBalls.Length; j++)
    //    {
    //        GameObject newParentBall = new GameObject("Pool " + prefabBalls[j].name);

    //        newParentBall.transform.parent = transform;

    //        parentFxPoint.Add(newParentBall.transform);

    //        collectionFxPoint.Add(new RallyFxPoint());

    //        for (int i = 0; i < preLoad; i++)
    //        {
    //            GameObject newObj = Instantiate(prefabBalls[j], parentFxPoint[j].position, Quaternion.identity, parentFxPoint[j]);

    //            newObj.SetActive(false);

    //            collectionFxPoint[j].FxPoints.Add(newObj);
    //        }
    //    }
    //}

    public void SpawnFx(int idBall, Vector2 posCreate, float valueScale)
    {
       var a =  SmartPool.Instance.Spawn(prefabBalls[idBall], posCreate, Quaternion.identity);
        a.transform.localScale = Vector3.one *0.001f;
       // GameObject newObj = Instantiate(prefabBalls[idBall], parentFxPoint[idBall].position, Quaternion.identity, parentFxPoint[idBall]);
    }


    public GameObject Spawn(int idBall, Vector2 posCreate, float valueScale)
    {
        GameObject obj = null;

        var listBall = collectionFxPoint[idBall].FxPoints;

        for (int i = 0; i < listBall.Count; i++)
        {
            if (listBall[i] != null && !listBall[i].activeInHierarchy)
            {
                obj = listBall[i];
            }
        }

        if (obj == null)
        {
            GameObject newObj = Instantiate(prefabBalls[idBall], parentFxPoint[idBall].position, Quaternion.identity, parentFxPoint[idBall]);

            collectionFxPoint[idBall].FxPoints.Add(newObj);

            obj = newObj;

            obj.SetActive(false);
        }

        obj.transform.position = posCreate;

        obj.transform.localScale = Vector2.one * 0.1f * valueScale;

        obj.SetActive(true);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        //obj.SetActive(false);
        SmartPool.Instance.Despawn(obj);
    }
}

[Serializable]
public class RallyFxPoint
{
    public List<GameObject> FxPoints = new List<GameObject>();
}
