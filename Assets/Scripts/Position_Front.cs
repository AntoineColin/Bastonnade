using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position_Front : Position {

	public override bool Punch (int side, float damage, Boxer target) {
		return base.Punch(side, damage+1, target);
	}
}