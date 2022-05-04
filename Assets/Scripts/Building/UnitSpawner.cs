using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] Transform spawnPoint;
    public void Spawn()
    {
      GameObject obj = Instantiate(objectPrefab,spawnPoint.position,spawnPoint.rotation);
      UnitSelectionController.Singleton.myAllUnits.Add(obj.GetComponent<UnitMover>());
    }
}
