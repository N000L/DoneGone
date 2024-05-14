using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
   public GameObject battleObject;
   public PlayerInfo inventory;

   private void Start()
   {
      battleObject = GameObject.FindGameObjectWithTag("BattleSystem");
            
      inventory = battleObject.GetComponent<PlayerInfo>();
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.F))
      {
         Debug.Log(inventory.playerLevel + " player level when called this way is this");
         SaveToJson();
      }
      
      if (Input.GetKeyDown(KeyCode.L))
      {
         LoadFromJson();
      }
   }

   public void SaveToJson()
   {
      string playerData = JsonUtility.ToJson(inventory);
      string filePath = Application.persistentDataPath + "/PlayerData.json";
      Debug.Log(filePath);
      System.IO.File.WriteAllText(filePath, playerData);
      Debug.Log("Saved");
   }

   public void LoadFromJson()
   {
      string filePath = Application.persistentDataPath + "/PlayerData.json";
      string playerData = System.IO.File.ReadAllText(filePath);

      //inventory = JsonUtility.FromJson<PlayerInfo>(playerData);
      JsonUtility.FromJsonOverwrite(playerData, battleObject.GetComponent<PlayerInfo>());
      Debug.Log("Loaded");
   }
}



[System.Serializable]
public class Items
{
   public string name;
   public string desc;
}
