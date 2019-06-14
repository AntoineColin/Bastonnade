using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_Back : Position {

	public override float GetBeaten(float damage){
		return damage-1;
	}
}