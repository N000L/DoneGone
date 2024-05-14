using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN, WON, LOST, NOTSTARTED
}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform enemyTransform;
    public BattleState state;
    private Unit playerUnit;
    private Unit enemyUnit;
    public Canvas battleCanvas;
    public GameObject battleObject;
    public PlayerInfo pinfo;
    public PlayerData pdata;
    
    public TextMeshProUGUI dialogueText;
    public List<GameObject> enemies;

    public BattleHUD enemyHUD;
    public BattleHUD playerHUD;
    public bool startBattle;
    
    // Start is called before the first frame update


    private void Start()
    {
        state = BattleState.NOTSTARTED;
        startBattle = true;
        battleObject = GameObject.FindGameObjectWithTag("BattleSystem");
            
        pinfo = battleObject.GetComponent<PlayerInfo>();
        print(pinfo);
        
        pdata = GetComponent<PlayerData>();
    }

    IEnumerator SetUpBattle()
    {
        if (pinfo.playerLevel <= 1)
        {
            pdata.LoadFromJson();
        }
        GameObject playerGO = Instantiate(playerPrefab, enemyTransform);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemies[Random.Range(0,enemies.Count)], enemyTransform);
        enemyUnit = enemyGO.GetComponent<Unit>(); 

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        
        dialogueText.text = "Battle start!";
        yield return new WaitForSeconds(1f);
        
        state = BattleState.PLAYERTURN;
        PlayerTurn();
        startBattle = true;

    }

    public void StartBattle()
    {
        if (startBattle)
        {
            StartCoroutine(SetUpBattle());
            startBattle = false;
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action :";
    }
    

    private void Update()
    {
        //Debug.Log(state);
        if (state == BattleState.START)
        {
            StartBattle();
        }
        
        OnAttackButton();

        if (state != BattleState.NOTSTARTED)
        {
            battleCanvas.gameObject.SetActive(true);
        }
        else
        {
            battleCanvas.gameObject.SetActive(false);
        }
    }

    void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerExtra()
    {
        playerUnit.Heal(8 + (playerUnit.unitLevel*3));
        
        playerHUD.SetHUD(playerUnit);
        dialogueText.text = "Healed";
        
        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (state == BattleState.PLAYERTURN)
            {
                dialogueText.text = "Player attacks!";
                yield return new WaitForSeconds(1f);
                bool isDead = enemyUnit.TakeDamage(playerUnit.damage);
                enemyHUD.SetHUD(enemyUnit);
                Debug.Log(playerUnit.currentHP);
                if (isDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                }
                else
                {
                    state = BattleState.ENEMYTURN;
                    StartCoroutine(EnemyTurn());
                }
            }
            else
            {
                dialogueText.text = "cant attack";
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            if (state == BattleState.PLAYERTURN)
            {
                StartCoroutine(PlayerExtra());
            }
            else
            {
                dialogueText.text = "cant action";
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (state == BattleState.PLAYERTURN)
            {
                EndBattle();
                
            }
            else
            {
                dialogueText.text = "cant action";
            }
        }
    }

    void EndBattle()
    {
        GameObject[] unitList = GameObject.FindGameObjectsWithTag("CombatUnit");

        // Iterate through the array of enemies and destroy each one
        foreach (GameObject i in unitList)
        {
            Destroy(i);
        }   
        
        unitList = GameObject.FindGameObjectsWithTag("PlayerUnit");
        
        foreach (GameObject i in unitList)
        {
            Destroy(i);
        }  
        
        if (state == BattleState.WON)
        {
            dialogueText.text = "you win";
            pinfo.playerXP += 25*enemyUnit.unitLevel;
        }

        if (state == BattleState.LOST)
        {
            StartCoroutine(Death());
        }

        pinfo.playerHP = playerUnit.currentHP;
        state = BattleState.NOTSTARTED;
        pdata.SaveToJson();
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " attacks!";
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        if (enemyUnit.unitName == "Blood Bug")
        {
            
            dialogueText.text = "Enemy drains life!";
            enemyUnit.Heal((enemyUnit.unitLevel) + 2);
            enemyHUD.SetHUD(enemyUnit);
            yield return new WaitForSeconds(1f);
        }

        playerHUD.SetHUD(playerUnit);
        
        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
            
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator Death()
    {
        if (pinfo.floorsMoved > 1)
        {
            pinfo.floorsMoved -= 1;
        }
        pinfo.playerHP = 20 + (pinfo.playerLevel*(1 + (pinfo.playerLevel/4)));
        pdata.SaveToJson();
        yield return new WaitForSeconds(2);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        
    }


}
