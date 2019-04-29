using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{

    Manager manager;
    public Text money;
    public GameObject gameOver;

    void Start(){
        manager = GameObject.FindObjectOfType<Manager>();
        UpdateUI();
    }

    public void UpdateUI(){
        money.text = "S" + manager.money;
        if(manager.money < 0){
            gameOver.SetActive(true);
        }
    }
}
