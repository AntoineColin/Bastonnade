using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer_Player : Boxer {
    public override void Lose () {
        Debug.Log("T'as perdu");
    }

    public void Update(){
        if(!isStunned()){
            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0){
                SetPositionIdle();
            }else{
                if(Input.GetAxisRaw("Horizontal") > 0){
                    SetPositionRight();
                }
                if(Input.GetAxisRaw("Horizontal") < 0){
                    SetPositionLeft();
                }
                if(Input.GetAxisRaw("Vertical") > 0){
                    SetPositionFront();
                }
                if(Input.GetAxisRaw("Vertical") < 0){
                    SetPositionBack();
                }
            }
            if(Input.GetButtonDown("RightPunch")){
                Punch(1);
            }else if(Input.GetButtonDown("LeftPunch")){
                Punch(2);
            }
        }
    }
}