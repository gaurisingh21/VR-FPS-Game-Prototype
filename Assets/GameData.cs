using System.Collections.Generic;
using System;

[Serializable]
public class GameData
{
    public List<TargetHitData> targetHits;
    public List<SphereHitData> sphereHits;
    public float timeTaken;
    public int score;
}

[Serializable]
public class TargetHitData
{
    public int targetId;
    public float hitTime; 
}

[Serializable]

public class SphereHitData
{
    public int hitID;
    public float sphereTime;
}