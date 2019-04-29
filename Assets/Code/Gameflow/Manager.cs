using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public int money;
    int respawnCharge = 100;

    public int nextLevel = 2;

    void Start()
    {
        if(GameObject.FindObjectOfType<Manager>()){
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    private void Awake() {
        DontDestroyOnLoad(gameObject);
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

    public void LoadNextLevel(){
        SceneManager.LoadScene("Level " + nextLevel);
        nextLevel++;
    }
}
