using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;
using TMPro; 

public class SpawnManager : MonoBehaviour
{
   [SerializeField] Transform[] spawnPoints;

   [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject[] dusmanPrefab;

    [SerializeField] float gecikme = 50f;
    [SerializeField] TextMeshProUGUI kalanSaniyeText;
    public int RandomDirection()
    {
        
        return Random.Range(0,spawnPoints.Length);
    }
    private void Start() {
       // Invoke("GetPoints",5f);
        InvokeRepeating("GetPoints", gecikme, gecikme);
        gerisayim = gecikme;
    }
    int a = 0;
    int c = 0;
    float gerisayim;
    private void Update()
    {
        gerisayim -= Time.deltaTime;
        int kalan = (int) gerisayim;
        kalanSaniyeText.text = kalan.ToString() + "  Seconds left for next wave";
    }
    void GetPoints()
    {
        gerisayim = gecikme;

        int b = RandomDirection();
        a = 0;
        c = 0;
        for (int i = 0; i < dusmanPrefab.Length; i++)
        {
            if (dusmanPrefab[i].activeSelf)
            {
                c++;
                continue;
            }
            if (a >= spawnPoints[b].childCount - 1)
                return;
            
                
            dusmanPrefab[i].transform.position = spawnPoints[b].GetChild(i-c).position;
            a++;
            dusmanPrefab[i].SetActive(true);

        }
        
        gecikme *= .9f;
        
        Debug.Log(gecikme);
    }
}
