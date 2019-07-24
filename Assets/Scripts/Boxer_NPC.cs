using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxer_NPC : Boxer
{
	//TODO Intelligence artificielle

	public override void Lose(){
		Debug.Log("Ennemi mort");
	}

	public new void Start(){
		base.Start();
		Debug.Log(isPosition(0));
		SetPosition(2);
	}

	
}
