using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    int maxLife;
		RawImage img;
		public bool left = true;

		void Start()
		{
			img = gameObject.GetComponent<RawImage>();	
		}

		public void setLife(int life){
			maxLife = life;
		}

		public void changeLife(int life){
			if(left)img.uvRect = new Rect(0,img.uvRect.x - life/maxLife,1,1);
			else img.uvRect = new Rect(0,img.uvRect.x - life/maxLife,1,1);
			if(img.uvRect.x < 0){
				img.uvRect = new Rect(0,0,1,1);
			}
			if(img.uvRect.x > 1){
				img.uvRect = new Rect(0,1,1,1);
			}
		}
}
