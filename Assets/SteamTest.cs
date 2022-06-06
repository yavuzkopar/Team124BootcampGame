using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Steamworks;

public class SteamTest : MonoBehaviour
{

    [SerializeField] Text text;
    // Start is called before the first frame update

    void Start()
    {
        if (!SteamManager.Initialized) return;
        text.text = "Welcome  " + SteamFriends.GetPersonaName();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
