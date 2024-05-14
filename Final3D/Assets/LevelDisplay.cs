using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelDisplay : MonoBehaviour
{
    public int playerLevel;
    public TextMeshProUGUI thisText;
    
    // Update is called once per frame
    void Update()
    {
        playerLevel = new PlayerInfo().playerLevel;
        thisText.text = "Lvl: " + playerLevel;
    }
}
