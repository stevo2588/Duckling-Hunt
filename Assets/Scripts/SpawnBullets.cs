using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnBullets: MonoBehaviour	{
	
	public GameObject opponentMomDuck;
	public GameObject bulletObject;
	public string colorTag = "yellow";
	public string opponentTag = "blue";
	
	public float waitTime = .05f;
	private float timeElapsed = 0.0f;
	
	public void Start(){
		//animation.wrapMode = WrapMode.Loop;

		GetComponent<Animation>()["shoot"].layer = -1;
		GetComponent<Animation>().SyncLayer(-1);
	
		// We are in full control here - don't let any other animations play when we start
		GetComponent<Animation>().Stop();
	}
	
	public void Update() {
		//while uncollected idle
		
		timeElapsed += Time.deltaTime;
		
		if(timeElapsed >= waitTime) {
			if(Input.GetButton(colorTag+"Fire")) {
				timeElapsed = 0;
				Createbullet();
			}
		}
	}

	public void Createbullet() {
		Vector3 newPos = transform.position-transform.forward;
		
		//Bullet bullet = Instantiate(bulletObject, newPos, transform.rotation) as Bullet;
		GameObject bullet = Instantiate(bulletObject, newPos, transform.rotation) as GameObject;
		Bullet bulletScript = bullet.GetComponent<Bullet>();
		bulletScript.setDirection(-transform.forward);
		
		print(GetComponent<Animation>().Play("shoot"));
	}

		
}
