using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build : MonoBehaviour
{
    Button button;
    private void Start() {
        button = GetComponent<Button>();
    }
    private void Update() {
        button.interactable = Economy.singleton.wood >= 100;
    }
    public void Buildd(GameObject preview)
    {
        Instantiate(preview);
    }
}
