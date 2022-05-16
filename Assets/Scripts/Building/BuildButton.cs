using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public Transform spawnPos;
    public Transform destPoint;
    GameObject objj;
    bool a = true;
    public void ButtonSelected(GameObject preview)
    {
        
       // objj = preview;
       if (a)
       {
            objj = Instantiate(preview,spawnPos.position,spawnPos.rotation);
            
            a = false;
            Invoke(nameof(Yap),2f);
       }
       
       
        
        
        
    }
    void Yap()
    {
        
        UnitMover unit = objj.GetComponent<UnitMover>();
        UnitSelectionController.Singleton.myAllUnits.Add(unit);
        unit.MoveToPoint(destPoint.position);
        a = true;
       
    }
  
   
}
