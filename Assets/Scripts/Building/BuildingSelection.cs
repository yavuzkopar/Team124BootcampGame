using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSelection : MonoBehaviour
{
     public UnitSpawner unitSpawner = null;
     [SerializeField] LayerMask layerMask;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Yap();
        }
    }
    void Yap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if(!Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,layerMask)){
            
           
            return;
        }

        if(!hit.collider.TryGetComponent<UnitSpawner>(out UnitSpawner unit))
        {
            
             return;
        }
        unitSpawner = unit;
        
    }
    public void Spawnla()
    {
        if(unitSpawner == null) return;

        unitSpawner.Spawn();
    }
}
