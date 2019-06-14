using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Position {
    public virtual float GetBeaten(float damage){
			return damage;
    }

    public virtual bool Punch (int side, float damage, Boxer target){
			Debug.Log("Paf !");
			return target.GetBeaten(damage, side);
    }


}