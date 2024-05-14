using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayerPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject battleObject;
    public BattleSystem battleSystem;
    public PlayerInfo pinfo;

    private void Awake()
    {
        battleObject = GameObject.FindGameObjectWithTag("BattleSystem");
            
        pinfo = battleObject.GetComponent<PlayerInfo>();

        battleSystem = battleObject.GetComponent<BattleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (battleSystem.state == BattleState.NOTSTARTED)
        {
            dialogueText.text = ("P LVL: " + pinfo.playerLevel  + "  E LVL: " + pinfo.floorsMoved + "  P HP: " + pinfo.playerHP);
        }
        else
        {
            dialogueText.text = " ";
        }
    }
}
