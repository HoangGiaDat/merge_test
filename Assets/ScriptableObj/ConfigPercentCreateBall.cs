using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigPercentCreateBall", menuName = "ScriptableObjects/GameConfig/PercentCreateBall", order = 0)]

public class ConfigPercentCreateBall : ScriptableObject
{
    public PercentPhase[] percentPhases;
}

[Serializable]
public class PercentPhase
{
    public int[] percentCreate;
}
