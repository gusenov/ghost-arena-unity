using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static System.Random RandomNum;

    public static int DifficultyLevel;

    public static Vector3[] StartingPoints = new Vector3[] {
        new Vector3(-2.575f, 3.4f, 0f), new Vector3(0f, 3.4f, 0f), new Vector3(2.575f, 3.4f, 0f),
        new Vector3(5.0f, 1.2f, 0f), new Vector3(5.0f, -1.14f, 0f),
        new Vector3(2.575f, -3.4f, 0), new Vector3(0f, -3.4f, 0f),new Vector3(-2.575f, -3.4f, 0f),
        new Vector3(-5.0f, -1.14f, 0f), new Vector3(-5.0f, 1.2f, 0f)};


    public static float[] SpawnTimes = new float[] { 2, 1, 0.5f };

    public static int Score;
    public static int Health;

    public static GameState CurGameState;

    // public static AudioSource BulletAudioSource;

    // public static Dictionary<LeaderboardType, List<LeaderboardData>> Leaderboards;
}

public enum GameState
{
    PlayingGame,
    PauseGame,
    GameOver
}

public enum BulletEventType
{
    Hit,
    Miss
}
