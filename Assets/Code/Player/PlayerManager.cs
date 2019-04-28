using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    Manager manager;
    Vector3 startPos;

    void Start(){
        startPos = transform.position;
        manager = GameObject.FindObjectOfType<Manager>();    
    }
    void Update(){
        
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Kill"){
            manager.Reload();
            transform.position = startPos;
        }else if(other.tag == "Coin"){
            manager.IncreaseMoney(100);
            Destroy(other.gameObject);
        }
    }
}
