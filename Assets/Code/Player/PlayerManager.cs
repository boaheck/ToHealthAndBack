using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    Manager manager;
    Vector3 startPos;
    public float controlDelay = 3.0f;
    public float corpseForce = 50.0f;
    float controlTimer;
    bool dead;
    PlatformerController player;
    public GameObject corpse;

    public AudioSource aud;

    void Start(){
        startPos = transform.position;
        manager = GameObject.FindObjectOfType<Manager>();   
        player = GameObject.FindObjectOfType<PlatformerController>();
        controlTimer = controlDelay; 
    }
    void Update(){
        if (controlTimer > 0){
            controlTimer-= Time.deltaTime;
        }else if(dead){
            if(Input.GetButtonDown("Respawn")){
                manager.Reload();
                transform.position = startPos;
                player.render = true;
                player.controllable = true;
                player.Freeze();
                dead = false;
                GameObject.FindObjectOfType<UIHandler>().UnDead();
            }
        }else if(!player.controllable){
            player.controllable = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(!dead){
            if(other.tag == "Kill"){
                dead = true;
                player.render = false;
                player.controllable = false;
                GameObject newCorpse = Instantiate(corpse, transform.position, Quaternion.identity);
                newCorpse.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.xVel,player.yVel) * corpseForce);
                player.Freeze();
                GameObject.FindObjectOfType<UIHandler>().Dead();
            }else if(other.tag == "Coin"){
                manager.IncreaseMoney(100);
                //aud.pitch = Random.Range(0.7f,1.0f);
                if(aud.isPlaying && aud.pitch < 1.0f){
                    aud.pitch += 0.02f;
                }else{
                    aud.pitch = 0.7f;
                }
                aud.Play();
                Destroy(other.gameObject);
            }else if(other.tag == "Finish"){
                manager.LoadNextLevel();
            }
        }
    }
}
