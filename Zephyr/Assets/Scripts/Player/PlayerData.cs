using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{

    private const int DIMENSIONS = 2;
    private const int X_COORD = 0;
    private const int Y_COORD = 1; 

    public float[] position;

    public PlayerData(GameManager gm) {
        position = new float[DIMENSIONS];
        position[X_COORD] = gm.lastCheckPointPos.x;
        position[Y_COORD] = gm.lastCheckPointPos.y; 

    }
}
