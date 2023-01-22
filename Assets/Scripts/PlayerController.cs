using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float  horizontalMove;
    public float  verticalMove;
    public CharacterController player;
    private Vector3 playerInput;
    public float playerSpeed;
    private Vector3 movePlayer;
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    public float gravity =9.8f;
    public float fallVelocity;

    void Start(){
        player = GetComponent<CharacterController>();
    }
 
    void Update(){
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        playerInput= new Vector3(horizontalMove,0,verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);
        CamDirection();
        movePlayer=playerInput.x * camRight + playerInput.z * camForward;
        movePlayer= movePlayer * playerSpeed;
        player.transform.LookAt(player.transform.position + movePlayer);
        SetGravity();
        player.Move(movePlayer* Time.deltaTime);        
    }

    void CamDirection(){
        camForward=mainCamera.transform.forward;
        camRight=mainCamera.transform.right;
        camForward.y=0;
        camRight.y=0;
        camForward = camForward.normalized;
        camRight=camRight.normalized;
    }

    void SetGravity(){
        if(player.isGrounded){
            fallVelocity= -gravity*Time.deltaTime;
            movePlayer.y=fallVelocity;
        }else{
            fallVelocity -= gravity * Time.deltaTime;
            movePlayer.y=fallVelocity;
        }

        
    }

    
}
