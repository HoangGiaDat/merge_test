using UnityEngine;
using System;

[Serializable] public class TypeCombo
{
    public ImgCombo[] combo;
}


public class ComboActive : MonoBehaviour
{
    [SerializeField] TypeCombo[] CollectionCombo;

    [SerializeField] Transform[] posEnd;

    float combo_expiration_time;

    int countCombo;

    [HideInInspector] public float lastTimeMerge;

    private void Awake()
    {
        combo_expiration_time = 0.5f;
    }

    ImgCombo GetImgCombo(int index)
    {
        ImgCombo obj = null;

        var listBall = CollectionCombo[index].combo;

        for (int i = 0; i < listBall.Length; i++)
        {
            if (listBall[i] != null && !listBall[i].gameObject.activeInHierarchy)
            {
                obj = listBall[i];
            }
        }

        return obj;
    }

    public void ShowCombo()
    {
        if (Time.time - lastTimeMerge > combo_expiration_time)
        {
            countCombo = 0;
        }

        countCombo++;

        if (countCombo >= 7) 
            countCombo = 7;

        lastTimeMerge = Time.time;

        int index = countCombo - 2;

        if (index < 0) return;

        var imgCombo = GetImgCombo(index);

        if(index == 5)
        {
            UserData.getUltimateCombo++;
        }

        if (imgCombo == null)
        {
            print("ImgCombo NULL");

            return;
        }

        imgCombo.Zoom(0.25f, 1.25f, posEnd[index]);
    }

}
