using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnDucks: MonoBehaviour	{
	
	List<GameObject> CollectedBabyDucks;
	List<GameObject> UncollectedBabyDucks;
	
	List<Vector3> previousPos;
	List<Quaternion> previousRot;
	
	public int uncollectedDuckMax=2;
	private int duckSpacing = 8;
	public int collectedDuckMax = 30;
	private int uncollectedDuckCount=0;
	private int minDistFromOtherDucks = 3;
	
	public GameObject babyDuck;
	public GameObject explodingDuck;
	
	public string colorTag = "yellow";
	
	private float YRayDist = 100;
	private GameObject groundPlane;
	
	public void Start(){
		CollectedBabyDucks = new List<GameObject>();
		UncollectedBabyDucks = new List<GameObject>();
		previousPos = new List<Vector3>();
		previousRot = new List<Quaternion>();
		
		groundPlane = GameObject.FindGameObjectWithTag("Ground");
		StartCoroutine(Spawn());
	}
	
	public void Update() {
		//while uncollected idle
		
		//collected ducks
		for(int i=0; i<CollectedBabyDucks.Count; i++)
		{
			
			CollectedBabyDucks[i].transform.position = previousPos[i*duckSpacing+duckSpacing];
			CollectedBabyDucks[i].transform.rotation = previousRot[i*duckSpacing+duckSpacing];
			
			//check for and respond to collision with bullet
			//GameObject bullets = GameObject.FindGameObjectsWithTag("Bullet");
		
		}
		
		previousPos.Insert(0,transform.position);
		previousRot.Insert(0,transform.rotation);
		
		if(previousPos.Count > collectedDuckMax*duckSpacing+duckSpacing)
		{
			previousPos.RemoveAt(collectedDuckMax*duckSpacing+duckSpacing-1);
			previousRot.RemoveAt(collectedDuckMax*duckSpacing+duckSpacing-1);
		}
		
		if(uncollectedDuckCount < uncollectedDuckMax)
		{
			StartCoroutine(Spawn());
		}
		
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//if mom collides with baby duck then collect baby duck
		/*if(hit.gameObject.tag.Equals(colorTag+"BabyDuck"))
		{
				//print ("Collided with Baby Duck!!");
				hit.gameObject.tag += "Collected";
				CollectedBabyDucks.Add(hit.gameObject);
				UncollectedBabyDucks.Remove(hit.gameObject);
				uncollectedDuckCount--;
				//print ("Duck Count: "+uncollectedDuckCount);
		}*/
		
		//bullet collide with mom duck
		if(hit.gameObject.tag.Contains("Bullet"))
		{
			//print ("Got Shot!");
			Destroy(hit.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider c) {
		if(c.gameObject.tag.Equals(colorTag+"BabyDuck"))
		{
				print ("Collided with Baby Duck!!");
				c.gameObject.tag += "Collected";
				CollectedBabyDucks.Add(c.gameObject);
				UncollectedBabyDucks.Remove(c.gameObject);
				print(c.gameObject.GetComponent<Animation>().Play("waddle"));
				uncollectedDuckCount--;
				//print ("Duck Count: "+uncollectedDuckCount);
			
				// send update score event
				Messenger<string,int>.Broadcast("ScoreChanged", colorTag, CollectedBabyDucks.Count);
		}
	}
	
	private IEnumerator Explode(Vector3 pos) {
		GameObject eDuck = Instantiate(explodingDuck, pos, transform.rotation) as GameObject;
		
		eDuck.GetComponent<Animation>()["explode"].layer = -1;
		eDuck.GetComponent<Animation>().SyncLayer(-1);
		eDuck.GetComponent<Animation>().Play("explode");
		
		yield return new WaitForSeconds(0.2f);
		
		Destroy(eDuck);
	}
	
	public void killDuck(GameObject duck)
	{
		print ("Baby duck Count: "+CollectedBabyDucks.Count.ToString());
		if(CollectedBabyDucks.Contains(duck))
		{
			//remove it from the baby duck list
			CollectedBabyDucks.Remove(duck);
			//destroy the game object
			Vector3 dPos = duck.transform.position;
			Destroy(duck);
			// send update score event
			Messenger<string,int>.Broadcast("ScoreChanged", colorTag, CollectedBabyDucks.Count);
			
			StartCoroutine(Explode (dPos));
		}

	}
	
	private IEnumerator Spawn(){
			if(uncollectedDuckCount < uncollectedDuckMax){
				CreateDuck();
				yield return new WaitForSeconds(3); // wait 3 seconds
				//yield return null;
			}
	}
	
	public void CreateDuck(){
		//generate random position but make sure that it is in valid area
		bool validLoc= false;
		Vector3 rayStartPos;
		Vector3 newPos = new Vector3(0,0,0);
		
		while(!validLoc)
		{
			rayStartPos.x= UnityEngine.Random.Range(groundPlane.GetComponent<Collider>().bounds.min.x+3,groundPlane.GetComponent<Collider>().bounds.max.x-3);
			rayStartPos.y = YRayDist;
			rayStartPos.z = UnityEngine.Random.Range(groundPlane.GetComponent<Collider>().bounds.min.z+3,groundPlane.GetComponent<Collider>().bounds.max.z-3);
		
			RaycastHit hit = new RaycastHit();
			bool test = Physics.Raycast(rayStartPos,Vector3.down,out hit,YRayDist+500);
			if(test)
			{	
				if(checkPosValid(hit))
				{
					newPos=rayStartPos;
					newPos.y=-.5f;
					validLoc = true;
				}
			}
		}
		
		GameObject duck = Instantiate(babyDuck, newPos, transform.rotation) as GameObject;
		duck.tag = colorTag+"BabyDuck";
		
		duck.GetComponent<Animation>().wrapMode = WrapMode.Loop;
		duck.GetComponent<Animation>()["waddle"].layer = -1;
		duck.GetComponent<Animation>().SyncLayer(-1);
		duck.GetComponent<Animation>().Stop();
		
		UncollectedBabyDucks.Add(duck);
		uncollectedDuckCount++;
	}
	
	public bool checkPosValid(RaycastHit r)
	{
		if(r.collider.gameObject.tag.Equals("Block"))
			return false;
		else if(r.collider.gameObject.tag.Equals("Ground"))
		{
			foreach(GameObject duck in UncollectedBabyDucks)
			{
				if(Vector3.Distance(duck.transform.position,r.point)<minDistFromOtherDucks)
					return false;
			}
			return true;
		}
		return false;
	}
	
	public int getNumDucks()
	{
		return CollectedBabyDucks.Count;
	}
		
}
