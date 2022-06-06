using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class BuildButton : MonoBehaviour
{
    
    GameObject objj;
    static bool a = true;
    public Birim birim;

    
    public void ButtonSelected()
    {
        if(!Economy.singleton.CanPopulate())return;
       // objj = preview;
       if (a)
       {
            objj = BirimiBul();
            objj.transform.position = AnaBina.Singleton.spawnPos.position;
            objj.SetActive(true);
            
            a = false;
            Invoke(nameof(Yap),2f);
       }
        
    }
    GameObject BirimiBul()
    {
        if (birim == Birim.Aslan)
        {
            for (int i = 0; i < Economy.singleton.aslanlar.Length; i++)
            {
                if (!Economy.singleton.aslanlar[i].activeSelf)
                {
                    objj = Economy.singleton.aslanlar[i];

                    return objj;

                }
            }
        }
        else if (birim == Birim.Ayi)
        {
            
            for (int i = 0; i < Economy.singleton.ayilar.Length; i++)
            {
                if(!Economy.singleton.ayilar[i].activeSelf)
                {
                    objj = Economy.singleton.ayilar[i];
                    
                    return objj;
                    
                }
            }
        }
        else if (birim == Birim.Kunduz)
        {
            for (int i = 0; i < Economy.singleton.kunduzlar.Length; i++)
            {
                if (!Economy.singleton.kunduzlar[i].activeSelf)
                {
                    objj = Economy.singleton.kunduzlar[i];

                    return objj;

                }
            }
        }
        
            return null;
    }
    void Yap()
    {
        
        UnitMover unit = objj.GetComponent<UnitMover>();
     //   UnitSelectionController.Singleton.myAllUnits.Add(unit);
    
        Economy.singleton.UpdatePopulation();
        unit.MoveToPoint(AnaBina.Singleton.destPoint.position);
        a = true;
       
    }
  
   
}
public enum Birim
{
    Aslan,
    Kunduz,
    Ayi
}