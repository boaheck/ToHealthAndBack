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
    float currentSpeed = 0.0f;
    float jumpPressed = 0.0f;
    bool jumping = false;
    bool jumpheld = false;

    //Horizontal Movement
    public float speed = 5.0f;
    public float runSpeed = 10.0f;

    //Vertical Movement
    public float gravity = 9.0f;
    public float fallMult = 2.0f;
    public float jumpEndMult = 2.5f;
    public float jumpSpeed = 10.0f;
    public float landJumpTime = 0.05f;
    public float coyoteTime = 0.03f;

    void Start(){
        //Setup
        character = GetComponent<CharacterController2D>();
        currentSpeed = speed;
    }

    void Update(){
        //Handle input
        running = Input.GetButton("Run");
        if(running){
            currentSpeed = runSpeed;
        }else{
            currentSpeed = speed;
        }
        if(Input.GetButtonDown("Jump")){
            jumpPressed = landJumpTime;
        }else if(jumpPressed > 0){
            jumpPressed -= Time.deltaTime;
        }
    }

    void FixedUpdate() {
        //handle this in fixed update to avoid differences in axis value during time between update and fixed update
        moving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0.001;

        //Handle Movement   
        if(moving){
            xVel = Mathf.Sign(Input.GetAxis("Horizontal")) * currentSpeed;
        } else {
            xVel = 0;
        }
        character.Move(new Vector2(xVel,yVel) * Time.fixedDeltaTime);
    }
}
