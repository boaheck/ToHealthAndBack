using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    //Components
    CharacterController2D character;

    //status
    float xVel = 0;
    float yVel = 0;
    bool moving = false;
    bool running = false;
    float speedMod = 0.0f;
    float jumpPressed = 0.0f;
    bool jumping = false;
    bool jumpheld = false;

    //Horizontal Movement
    public float speed = 10.0f;
    public float runSpeedMod = 2.0f;
    public float wallJumpKick = 5.0f;
    public float airAcceleration = 10;
    public float airDirAcceleration = 30;
    public float airDeceleration = 1;

    //Vertical Movement
    public float fallSpeed = 20;
    public float gravity = 9.0f;
    public float fallMult = 2.0f;
    public float jumpEndMult = 2.5f;
    public float jumpSpeed = 10.0f;
    public float wallJumpSpeed = 5.0f;
    public float landJumpTime = 0.05f;
    public float coyoteTime = 0.03f;

    void Start(){
        //Setup
        character = GetComponent<CharacterController2D>();
        speedMod = speed;
    }

    void Update(){
        //Handle input
        running = Input.GetButton("Run");
        if(running){
            speedMod = runSpeedMod;
        }else{
            speedMod = 1.0f;
        }
        if(Input.GetButtonDown("Jump")){
            jumpPressed = landJumpTime;
        }else if(jumpPressed > 0){
            jumpPressed -= Time.deltaTime;
        }
        if(jumpheld && Input.GetButtonUp("Jump")){
            jumpheld = false;
        }
    }

    void FixedUpdate() {
        //handle this in fixed update to avoid differences in axis value during time between update and fixed update
        moving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.001;

        //Handle Movement   

        if(character.grounded){
            yVel = 0;
            jumping = false;
            if(moving){
                xVel = Mathf.Sign(Input.GetAxis("Horizontal")) * speed * speedMod;
            } else {
                xVel = 0;
            }
        }else{
            if(moving){
                float targetSpeed = Mathf.Sign(Input.GetAxis("Horizontal")) * speed * speedMod;
                if (Mathf.Sign(targetSpeed) != Mathf.Sign(xVel)){
                    xVel = Mathf.MoveTowards(xVel,targetSpeed,airDirAcceleration * speedMod * Time.fixedDeltaTime);
                }else{
                    xVel = Mathf.MoveTowards(xVel,targetSpeed,airAcceleration * speedMod * Time.fixedDeltaTime);
                }
            } else {
                xVel = Mathf.MoveTowards(xVel,0,airDeceleration * speedMod * Time.fixedDeltaTime);
            }
            if(character.head){
                yVel = 0;
            }
            if(yVel <= 0){
                yVel = Mathf.MoveTowards(yVel,-fallSpeed,gravity * fallMult * Time.fixedDeltaTime);
            }else if(jumpheld){
                yVel = Mathf.MoveTowards(yVel,-fallSpeed,gravity * Time.fixedDeltaTime);
            }else{
                yVel = Mathf.MoveTowards(yVel,-fallSpeed,gravity * jumpEndMult * Time.fixedDeltaTime);
            }
        }

        if(character.left){
            if(xVel < 0){
                xVel = 0;
            }
        }
        if(character.right){
            if(xVel > 0){
                xVel = 0;
            }
        }
        
        if(jumpPressed > 0){
            if(character.timeSinceLastGrounded < coyoteTime){
                yVel = jumpSpeed;
                jumpPressed = 0.0f;
                jumpheld = Input.GetButton("Jump");
                jumping = true;
            }else if(character.left){
                yVel = wallJumpSpeed;
                xVel = wallJumpKick * speedMod;
                jumpPressed = 0.0f;
                jumpheld = Input.GetButton("Jump");
                jumping = true;
            }else if(character.right){
                yVel = wallJumpSpeed;
                xVel = -wallJumpKick * speedMod;
                jumpPressed = 0.0f;
                jumpheld = Input.GetButton("Jump");
                jumping = true;
            }
        }

        character.Move(new Vector2(xVel,yVel) * Time.fixedDeltaTime);
    }
}
