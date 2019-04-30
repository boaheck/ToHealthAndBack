using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{

    public int money;
    int respawnCharge = 200;

    public int nextLevel = 2;

    void Start()
    {
        if(GameObject.FindObjectsOfType<Manager>().Length > 1){
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
        if(money < 0){
            SceneManager.LoadScene("Debt");
            Destroy(gameObject);
        }
    }

    public void Reload(){
        IEnumerable<IResetable> resets = FindObjectsOfType<MonoBehaviour>().OfType<IResetable>();
        foreach (IResetable r in resets){
            r.Reload();
        }
        DecreaseMoney(respawnCharge);
    }

    public void LoadNextLevel(){
        if(nextLevel == 6){
            SceneManager.LoadScene("Win");
            Destroy(gameObject);
        }
        SceneManager.LoadScene("Level " + nextLevel);
        respawnCharge = nextLevel * 200;
        nextLevel++;
    }
}
