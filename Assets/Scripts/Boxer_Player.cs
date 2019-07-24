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
				SetPosition(0);
			}else{
				if(Input.GetAxisRaw("Horizontal") > 0){
					SetPosition(3);
				}
				if(Input.GetAxisRaw("Horizontal") < 0){
					SetPosition(4);
				}
				if(Input.GetAxisRaw("Vertical") > 0){
					SetPosition(1);
				}
				if(Input.GetAxisRaw("Vertical") < 0){
					SetPosition(2);
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