using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
  public Boxer owner;
	int[] ids;

	//bonus ID        0           1                2              3         4
	public Bonus punchBonus, beatenBonus, setPositionBonus, startBonus, endBonus;

	public abstract void Punch(int side);
	public abstract void SetPosition(int positionID);
	public abstract void Starting();
	public abstract bool GetBeaten(float damage, int side);
	public abstract void DuelStart(Boxer ennemy);
	public abstract void DuelFinish();
}
