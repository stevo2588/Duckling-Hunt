using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Bullet: MonoBehaviour	{
	
	public GameObject opponentMomDuck;
	public GameObject hunter;
	public string colorTag = "yellow";
	public string opponentTag = "blue";
	static public float speed = 5.0f;
	private Vector3 moveDirection;
	private SpawnDucks ScriptName;
	
	public void Start(){
		move();
		ScriptName = GameObject.FindWithTag(opponentTag+"MomDuck").GetComponent<SpawnDucks>();
	}
	
	public void Update() {
		
	}
	
	void OnCollisionEnter(Collision other) {
		//Does this collide with itself??
		print ("Collided with: "+other.gameObject.ToString());
		
		//if bullet collides with opponent mom duck
		//if bullet collides with own mom duck
		
		//need to destroy this object
		Destroy(gameObject);
	}
	
	void OnTriggerEnter(Collider c) {
		print ("Trigger! "+c.gameObject.tag);
		//if bullet collides with opponent baby duck 
		if(c.gameObject.tag.Equals(opponentTag+"BabyDuckCollected")) {
			print ("Shot baby duck");
			ScriptName.killDuck(c.gameObject);
		}
		
		//if bullet collides with own baby duck
	}
	
	public void setDirection(Vector3 direction)	
	{
		moveDirection = direction;
	}
	
	public void move()
	{
		//CharacterController controller = GetComponent<CharacterController>();
		//controller.Move(moveDirection * speed);
		transform.GetComponent<Rigidbody>().velocity = moveDirection * speed;
		GetComponent<Rigidbody>().AddForce(moveDirection);
		//print(moveDirection);
	}
		
}
