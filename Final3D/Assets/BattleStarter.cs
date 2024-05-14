using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStarter : MonoBehaviour
{
    public GameObject player;
    public GameObject battleSystemObject;
    private BattleSystem battleSystem;
    private PlayerController playerController;
    public int countTillBattle;
    

    private void Start()
    {
        battleSystem = battleSystemObject.GetComponent<BattleSystem>();
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (playerController.stepCount > countTillBattle)
        {
            battleSystem.state = BattleState.START;
            playerController.stepCount = 0;
        }
    }
}
