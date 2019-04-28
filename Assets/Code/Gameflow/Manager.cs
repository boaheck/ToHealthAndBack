using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public int money;
    int respawnCharge = 100;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void IncreaseMoney(int amt){
        money += amt;
        GameObject.FindObjectOfType<UIHandler>().UpdateUI();
    }

    public void DecreaseMoney(int amt){
        money -= amt;
        GameObject.FindObjectOfType<UIHandler>().UpdateUI();
    }

    public void Reload(){
        IEnumerable<IResetable> resets = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();
        foreach (IResetable r in resets){
            r.Reload();
        }
        DecreaseMoney(respawnCharge);
        respawnCharge *= 2;
    }
}
