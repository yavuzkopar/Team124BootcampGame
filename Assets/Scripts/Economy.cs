using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    public static Economy singleton;
    public int wood = 0;
    public int population = 0;
    public int populationLimit = 5;
    public TMPro.TextMeshProUGUI woodMiktar;
    public TMPro.TextMeshProUGUI populationText;



    private void Awake() {
        singleton = this;

        woodMiktar.text = wood.ToString();
         populationText.text =   "0 / " + populationLimit.ToString();
    }
    public bool CanPopulate()
    {
        return population < populationLimit;
    }
    public void UpdatePopulation()
    {
        population = UnitSelectionController.Singleton.myAllUnits.Count;
        populationText.text = population.ToString() + " / " + populationLimit.ToString();
    }
    
}
