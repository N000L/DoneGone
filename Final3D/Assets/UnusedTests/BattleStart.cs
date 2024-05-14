using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStart : MonoBehaviour
{

    public GameObject player;
    public GameObject battleSystem;
    public bool battleStartTest;


    private void Update()
    {
       // Debug.Log(player.transform.position);
        //Debug.Log(transform.position);
        
        if ((player.transform.position == this.transform.position) && (battleStartTest))
        {
            battleSystem.GetComponent<BattleSystem>().state = BattleState.START;
            battleStartTest = false;
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("battleStart");

        if (battleSystem.GetComponent<BattleSystem>().state == BattleState.NOTSTARTED)
        {
            battleSystem.GetComponent<BattleSystem>().state = BattleState.START;
        }

        
        
    }
}
