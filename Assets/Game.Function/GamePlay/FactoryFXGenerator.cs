using Core.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;
using UnityEngine.UIElements;

public class FactoryFXGenerator : MonoBehaviour
{
    public static FactoryFXGenerator instance = null;

    [SerializeField] GameObject prefabFxMerge;

    List<GameObject> listFxMerge = new List<GameObject>();

    private void Awake()
    {
        instance = this;

        //CreatePool(5);
    }

    //void CreatePool(int preLoad)
    //{
    //    for (int i = 0; i < preLoad; i++)
    //    {
    //        GameObject newObj = Instantiate(prefabFxMerge, transform.position, Quaternion.identity, transform);

    //        newObj.SetActive(false);

    //        listFxMerge.Add(newObj);
    //    }
    //}

    public void SpawnFx(Vector2 posCreate ,float scale)
    {
       var fx = SmartPool.Instance.Spawn(prefabFxMerge.gameObject, posCreate, Quaternion.identity);
        fx.transform.localScale = Vector3.one * scale; ;
    }

    public GameObject Spawn(Vector2 posCreate, float scale)
    {
        GameObject obj = null;

        for (int i = 0; i < listFxMerge.Count; i++)
        {
            if (listFxMerge[i] != null && !listFxMerge[i].activeInHierarchy)
            {
                obj = listFxMerge[i];
            }
        }

        if (obj == null)
        {
            GameObject newObj = Instantiate(prefabFxMerge, transform.position, Quaternion.identity, transform);

            listFxMerge.Add(newObj);

            obj = newObj;
        }

        obj.transform.position = posCreate;

        obj.transform.localScale = Vector3.one * scale;

        obj.SetActive(true);

        return obj;
    }

    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
    }

}
