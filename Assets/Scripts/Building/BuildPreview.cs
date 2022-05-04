using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPreview : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject building;
    public static bool canBuild = false;
    
    
    void OnEnable()
    {
        canBuild = true;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isHitted = Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity, layerMask);
        if(!isHitted)
        {
            
         return;
        }
        
        
        if (Input.GetMouseButtonDown(0))
        {
            if (canBuild)
            {
                Instantiate(building,transform.position,transform.rotation);
                Destroy(gameObject);
                canBuild = false;
            }
            
        }
        transform.position = hit.point;
        Quaternion rot = Quaternion.LookRotation(hit.normal) *  Quaternion.Euler(90,0,0);
        transform.rotation = rot;
        
    }
    
    void OnTriggerStay(Collider other)
    {
        if (!Contains(other.gameObject.layer,layerMask))
        {
            canBuild= false;
            Debug.Log("olmadı");
        }
       // Debug.Log("oldu");
    }
    void OnTriggerExit(Collider other)
    {
        if (!Contains(other.gameObject.layer,layerMask))
        {
            canBuild = true;
            Debug.Log("oldu");
        }
    }
    public bool Contains(LayerMask mask, int layer)
     {
         return mask == (mask | (1 << layer));
     }
}
