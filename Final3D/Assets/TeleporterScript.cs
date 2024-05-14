using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{
    public GameObject player;
    public Light light;
    public bool isColliding;
    public Collider[] hitColliders;
    public GameObject colliderChecker;
    public GameObject battleObject;
    public PlayerInfo pinfo;
    public PlayerData pdata;
    public int counter;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        isColliding = false;
        
        battleObject = GameObject.FindGameObjectWithTag("BattleSystem");
            
        pinfo = battleObject.GetComponent<PlayerInfo>();
        print(pinfo);

        pdata = GetComponent<PlayerData>();

        if (pinfo.playerHP < 1)
        {
            pinfo.playerHP = 20 + (pinfo.playerLevel*(1 + (pinfo.playerLevel/4)));
        }
    }

    private void Update()
    {
        hitColliders = Physics.OverlapSphere(colliderChecker.transform.position, 0.25f);
        if (hitColliders.Length > 0)
        {
            if (hitColliders[0].gameObject.tag == "Player")
            {
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (pinfo.playerLevel > pinfo.floorsMoved)
                    {
                        pdata.LoadFromJson();
                        isColliding = true;
                        pinfo.floorsMoved += 1;
                        pinfo.playerHP = 20 + (pinfo.playerLevel*(1 + (pinfo.playerLevel/4)));
                        pdata.SaveToJson();

                        StartCoroutine(LightRoutine());

                    }
                }
            }
            
        }
        if (counter >= 100)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    IEnumerator LightRoutine()
    {
        for (int i = 0; i < 100; i++)
        {
            counter += 1;
            light.intensity += 0.1f;
            light.range += 10f;
            yield return new WaitForSecondsRealtime(0.01f);
        }
        
    }
}
