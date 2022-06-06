using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsActiveControl : MonoBehaviour,ISaveable
{
    public object CaptureState()
    {
        return gameObject.activeSelf;
    }

    public void RestoreState(object state)
    {
        gameObject.SetActive((bool)state);
    }

   
}
