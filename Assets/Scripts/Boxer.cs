using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

[System.Serializable]
public class IntEvent : UnityEvent<int>{}

public abstract class Boxer : MonoBehaviour {

	#region Declaration
		public UnityEvent onDie, onStun, onHurt, onDuelStart, onDuelEnd;
		public IntEvent onChangePosition, onPunch, onHit, onSetLife, onChangeLife;

		Vector3 startingPosition, startingScale;
		public float maxLife = 10f;
		float life,  shield;
		float cooldDownPunch=0.5f, timeStampPunch = 0f, punchPower=2f;
		bool stunned;
		
		//Position ID                     0                    1                      2                   3                           4
		Position[] positions = { new Position_Idle (), new Position_Front (), new Position_Back (), new Position_Right (), new Position_Left () };
		Position currentPosition;

		public Boxer foe;
		//bonus ID        0           1                2              3         4
		public Bonus punchBonus, beatenBonus, setPositionBonus, startBonus, endBonus;

	#endregion

	#region Setters
		public void SetPosition(int positionID){
			if(setPositionBonus==null){
				currentPosition = positions[positionID];
				Debug.Log(startingScale);
				switch(positionID){
					case 0:
					transform.position = startingPosition;
					transform.localScale = startingScale;
					break;
					case 1:
					transform.position = startingPosition;
					transform.localScale = startingScale + new Vector3(-0.1f, -0.1f);
					break;
					case 2:
					transform.position = startingPosition;
					transform.localScale = startingScale + new Vector3(0.1f, 0.1f);
					break;
					case 3:
					transform.position = startingPosition+ new Vector3(1,0);
					transform.localScale = startingScale;
					break;
					case 4:
					transform.position = startingPosition + new Vector3(-1,0);
					transform.localScale = startingScale;
					break;
				}
			}else{
				setPositionBonus.SetPosition(positionID);
			}
		}
		/* On peut brancher sur la plupart des fonctions de Boxer un bonus
				Les fonctions n'appellent le bonus que si il y en a un, sinon elles se déroulent normalement
				Les bonus possèdent leur version de la fonction qu'ils altèrent
				Si plusieurs bonus sont présents : le premier bonus appel le suivant
				Les bonus peuvent aussi avoir un effet instantané (exemple : augmentation des dégâts) 
		 */
		public void SetBonus(int[] bonusModificatorID, Bonus bonus){
			bonus.owner = this;
			foreach(int modificator in bonusModificatorID){
				switch(modificator){
					case 0 :{ //punchBonus
						Bonus pb = punchBonus;
						while(pb.punchBonus!=null){
							pb = pb.punchBonus;
						}
						pb = bonus;
						break;
					}
					case 1 :{ //beatenBonus
						Bonus pb = beatenBonus;
						while(pb.beatenBonus!=null){
							pb = pb.beatenBonus;
						}
						pb = bonus;
						break;
					}
					case 2 :{ //setPositionBonus
						Bonus pb = setPositionBonus;
						while(pb.setPositionBonus!=null){
							pb = pb.setPositionBonus;
						}
						pb = bonus;
						break;
					}
					case 3 :{ //startBonus
						Bonus pb = startBonus;
						while(pb.startBonus!=null){
							pb = pb.startBonus;
						}
						pb = bonus;
						break;
					}
					case 4 :{ //endBonus
						Bonus pb = endBonus;
						while(pb.endBonus!=null){
							pb = pb.endBonus;
						}
						pb = bonus;
						break;
					}
					//TODO suite des modificateurs bonus
				}
			}
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
		if(onSetLife != null) onSetLife.Invoke((int)maxLife);
		//SetPosition(0);
		startingPosition = transform.position;
		startingScale = transform.localScale;
		if(startBonus!=null)startBonus.Starting();
	}
	
	public void Heal (float healing) {
		life += healing;
		if(onChangeLife!=null)onChangeLife.Invoke(-(int)healing);
		if (life > maxLife) life = maxLife;
	}

	//Recevoir un coup (même esquivé)
	public bool GetBeaten(float damage, int side){
		if(beatenBonus!=null){
			return beatenBonus.GetBeaten(damage, side);
		}else{
			if((side == 1 && isPosition(3)) || (side == 2 && isPosition(4))){
				Debug.Log("Coup manqué !");
				return false;
			}else{
				Suffer(currentPosition.GetBeaten(damage));
				return true;
			}
		}
	}

	//Appelé pour réduire les pv
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
			if(onPunch != null){onPunch.Invoke(side);}
			if(punchBonus!=null){
				punchBonus.Punch(side);
			}else{
				if(currentPosition.Punch(side, punchPower, foe)){
					if(onHit != null){onHit.Invoke(side);}
				}else{
					Debug.Log("esquivé");
				}
			}
		}
	}

	public void DuelStart(Boxer ennemy){
		foe = ennemy;
		if(startBonus != null){
			startBonus.DuelStart(ennemy);
		}
	}

	public void DuelFinish(){
		foe= null;
		if(endBonus!=null){
			endBonus.DuelFinish();
		}
	}


}