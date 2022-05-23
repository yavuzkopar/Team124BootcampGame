using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
   [SerializeField] Transform[] spawnPoints;

   [SerializeField] GameObject enemyPrefab;

    public int RandomDirection()
    {
        
        return Random.Range(0,spawnPoints.Length);
    }
    private void Start() {
        Invoke("GetPoints",5f);
    }

    void GetPoints()
    {
        foreach (Transform item in spawnPoints[RandomDirection()])
        {
            GameObject obj = Instantiate(enemyPrefab,item.position,Quaternion.identity);
            obj.GetComponent<SaveableEntity>().SetSaving();
        }
    }
}
