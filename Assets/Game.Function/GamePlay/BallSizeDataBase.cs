using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DataSizeBall", menuName = "Data/DataSizeBall", order = 1)]
public class BallSizeDataBase : ScriptableObject
{
    public float[] size1;
    public float[] size2;
}
