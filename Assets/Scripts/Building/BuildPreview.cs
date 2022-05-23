using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject building;
    public static bool canBuild = false;

    MeshRenderer renderer;
    
    bool builded = false;
    [SerializeField] GameObject vfx;
    GameObject obj;
    void OnEnable()
    {
        canBuild = true;
        obj = GameObject.FindGameObjectWithTag("Ggg");
        renderer = GetComponentInChildren<MeshRenderer>();
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHitted = Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity, layerMask);
        if(!isHitted || UnitSelectionController.Singleton.isPointerOverUI)
        {
            
         return;
        }
    //    if (obj.CompareTag("Ggg"))
    //    {
    //        canBuild = true;
    //        renderer.material.color = Color.green;
    //    }
    //    else
    //    {
    //        canBuild = false;
    //        renderer.material.color = Color.red;
    //        
    //    }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (canBuild && Economy.singleton.wood >= 100)
            {
                builded = true;
                vfx.SetActive(true);
                Economy.singleton.wood -= 100;
                Invoke(nameof(Build),3f);
            }
            else
            {
                Destroy(gameObject);
            }
            
        }
        if (!builded)
        {
            transform.position = hit.point;
        Quaternion rot = Quaternion.LookRotation(hit.normal) *  Quaternion.Euler(90,0,0);
        transform.rotation = rot;
        }
        
        
    }
    void Build()
    {
        Instantiate(building,transform.position,transform.rotation);
        Economy.singleton.populationLimit += 5;
        Economy.singleton.UpdatePopulation();
        Destroy(gameObject);
        canBuild = false;
        
    }
    

  
    private void OnTriggerEnter(Collider other) {
         if (!other.gameObject.CompareTag("Ggg"))
        {
            renderer.material.color = Color.red;
            canBuild= false;
            Debug.Log("olmadı");
        }
         else
        {
            canBuild = true;
            renderer.material.color = Color.green;
        }

    }
   void OnTriggerStay(Collider other)
   {
       if (!other.gameObject.CompareTag("Ggg"))
       {
           renderer.material.color = Color.red;
           canBuild= false;
           Debug.Log("olmadı");
       }
       else
       {
            renderer.material.color = Color.green;
           canBuild= true;
       }
    // Debug.Log("oldu");
   }
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Ggg"))
        {
            canBuild = true;
            renderer.material.color = Color.green;
            Debug.Log("oldu");
        }
        else
        {
            canBuild = false;
            renderer.material.color = Color.red;
        }
    }
    public bool Contains(LayerMask mask, int layer)
     {
         return mask == (mask | (1 << layer));
     }
}
