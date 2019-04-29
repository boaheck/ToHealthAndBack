using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    Manager manager;
    Vector3 startPos;
    public float controlDelay = 3.0f;
    float controlTimer;
    PlatformerController player;

    void Start(){
        startPos = transform.position;
        manager = GameObject.FindObjectOfType<Manager>();   
        player = GameObject.FindObjectOfType<PlatformerController>();
        controlTimer = controlDelay; 
    }
    void Update(){
        if (controlTimer > 0){
            controlTimer-= Time.deltaTime;
        }else if(!player.controllable){
            player.controllable = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Kill"){
            manager.Reload();
            transform.position = startPos;
        }else if(other.tag == "Coin"){
            manager.IncreaseMoney(100);
            Destroy(other.gameObject);
        }else if(other.tag == "Finish"){
            manager.LoadNextLevel();
        }
    }
}
