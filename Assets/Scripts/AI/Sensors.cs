using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour
{
    public List<Health> enemyUnits = new List<Health>();
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("My Units"))
        {
            Health h = other.GetComponent<Health>();
            
            enemyUnits.Add(h);
        }
    }
     private void OnTriggerExit(Collider other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("My Units"))
        {
            Health h = other.GetComponent<Health>();
            enemyUnits.Remove(h);
        }
    }
}
