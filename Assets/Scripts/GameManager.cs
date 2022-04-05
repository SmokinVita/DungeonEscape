﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is null!");

            return _instance;
        }
    }

    public bool HasKeyToCastle { get; set; }
    public bool HasFlameSword { get; set; }
    public bool HasBootsOfFlight { get; set; }
    public Player Player { get; private set; }

    void Awake()
    {
        _instance = this;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    

}
