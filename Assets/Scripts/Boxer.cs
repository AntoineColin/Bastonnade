using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class IntEvent : UnityEvent<int>{}

public abstract class Boxer : MonoBehaviour {

	#region Declaration
		public UnityEvent onDie, onStun, onHurt;
		public IntEvent onChangePosition, onPunch;

		public float maxLife = 10f;
		float life,  shield;
		float cooldDownPunch=0.5f, timeStampPunch = 0f, punchPower=2f;
		bool stunned;
		
		Position[] positions = { new Position_Idle (), new Position_Front (), new Position_Back (), new Position_Right (), new Position_Left () };
		Position currentPosition;

		public Boxer foe;
	#endregion

	#region Position
		public void SetPositionIdle(){
			currentPosition = positions[0];
		}
		public void SetPositionFront(){
			currentPosition = positions[1];
		}
		public void SetPositionBack(){
			currentPosition = positions[2];
		}
		public void SetPositionRight(){
			currentPosition = positions[3];
		}
		public void SetPositionLeft(){
			currentPosition = positions[4];
		}
	#endregion
	
	#region State
		public bool isStunned(){
			return false;
		}
		public bool isPosition(int positionID){
			// idle=0, front=1, back=2, right=3, left=4
			return currentPosition == positions[positionID];
		}
		public bool isFullLife(){
			return maxLife == life;
		}
		public bool isLowLife(){
			return life*5 <= maxLife;
		}
		public bool isSided(){
			return isPosition(3)||isPosition(4);
		}
	#endregion

	public void Start () {
		life = maxLife;
		SetPositionIdle();
	}
	
	//TODO patern strategy pour les positions
	public void Heal (float healing) {
		life += healing;
		if (life > maxLife) life = maxLife;
	}

	public void GetBeaten (float damage) {
		Suffer(currentPosition.GetBeaten(damage));
	}

	public bool GetBeaten(float damage, int side){
		if((side == 1 && isPosition(3)) || (side == 2 && isPosition(4))){
			Debug.Log("Coup manqué !");
			return false;
		}else{
			GetBeaten(damage);
			return true;
		}

	}

	public void Suffer (float damage) {
		life -= damage;
		if (life <= 0) {
			Lose ();
		}
	}

	public abstract void Lose ();

	public void Punch(int side){
		if(timeStampPunch <= Time.time){
			timeStampPunch = Time.time + cooldDownPunch;
			if(currentPosition.Punch(side, punchPower, foe)){
				if(onPunch != null){onPunch.Invoke(side);}
			}else{
				Debug.Log("esquivé");
			}
			
		}
	}
}