using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButton : MonoBehaviour
{
    public void ButtonSelected(GameObject preview)
    {
        
        Instantiate(preview);
        
        
    }
}
