using System;
using System.Collections.Generic;
using UnityEngine;

public enum LeaderboardType
{
    GhostsDestroyed,
    PlayerSurvivalTime,
    Accuracy,
    GhostSurvivalTime
}

public enum LeaderboardAmountType
{
    FloatAmount,
    IntAmount
}
public class LeaderboardData
{
    public string PlayerName;
    public LeaderboardType Type;
    public LeaderboardAmountType AmountType;
    public float FloatAmount;
    public int IntAmount;

    public LeaderboardData()
    {
    }

    public LeaderboardData(LeaderboardType type)
    {
        Load(type);
    }

    public void Load(LeaderboardType type)
    {


    }

    public void Save()
    {

    }
}
