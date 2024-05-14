using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    
    public int damage;

    public int maxHP;
    public int currentHP;

    //public PlayerInfo pinfo;
    public GameObject battleObject; 
    
    private void Awake()
    {
        battleObject = GameObject.FindGameObjectWithTag("BattleSystem");
            
        var pinfo = battleObject.GetComponent<PlayerInfo>();
        print(pinfo);

        if (this.gameObject.tag == "CombatUnit")
        {
            unitLevel = pinfo.floorsMoved;
            
            damage = (damage*2) + (unitLevel*(1 + (unitLevel/2))) + (Random.Range(-unitLevel, unitLevel));
            maxHP = (maxHP*2) + (unitLevel*(1 + (unitLevel/2)));
            currentHP = maxHP;
        }

        if (unitName == "Player")
        {
            unitLevel = pinfo.GetPlayerLevel();

            maxHP = 20 + (pinfo.playerLevel*(1 + (pinfo.playerLevel/4)));
            damage = (damage*2) + (unitLevel*(1 + (unitLevel/2)));
            currentHP = pinfo.playerHP;
        }
    }


    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }

}
