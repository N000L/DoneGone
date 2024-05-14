using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo : MonoBehaviour
{
    
    
    public int playerLevel;
    public int playerXP;
    public int playerHP;
    public int floorsMoved;
    public List<Items> items = new List<Items>();

    private void Update()
    {
        if (playerXP >= 100*playerLevel)
        {
            playerLevel += 1;
            playerXP = 0;
            playerHP = 20 + (playerLevel*(1 + (playerLevel/4)));
        }
    }

    public int GetPlayerLevel()
    {
        Debug.Log("in class level is equal to " + playerLevel);
        int x = playerLevel;
        return x;
    }
}
