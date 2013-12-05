using UnityEngine;
using System;

public class LevelManager: MonoBehaviour {
	
	private static LevelManager instance;
	
	public float CountDownSeconds = 10.0f;
	private float curLevelStartTime = 0.0f;
	
	private int yellowLevelPoints = 0;
	private int blueLevelPoints = 0;
	
	public static LevelManager Instance	{
		get	{
			/*if (instance == null) {
				instance = new GameObject("LevelManager").AddComponent<LevelManager> ();
			}*/
			return instance;
		}
	}
 
	public void OnApplicationQuit()	{
		instance = null;
	}
	
	void Awake() {
        instance = gameObject.GetComponent<LevelManager>();
		curLevelStartTime = Time.time;
		yellowLevelPoints = 0;
		blueLevelPoints = 0;
	}
	
	void Start() {
	}
	
	void OnEnable() {
		// register event listeners
		Messenger<string,int>.AddListener("ScoreChanged", OnScoreChanged);
	}
	
	void Update(){
		if(this.LevelRemainingTime <= 0) {
			if(Application.loadedLevelName == "farmLevel") {
				MainGame.Instance.YellowLevel1 = yellowLevelPoints;
				MainGame.Instance.BlueLevel1 = blueLevelPoints;
			}
			else if(Application.loadedLevelName == "lakeLevel") {
				MainGame.Instance.YellowLevel2 = yellowLevelPoints;
				MainGame.Instance.BlueLevel2 = blueLevelPoints;
			}
			else if(Application.loadedLevelName == "spaceLevel") {
				MainGame.Instance.YellowLevel3 = yellowLevelPoints;
				MainGame.Instance.BlueLevel3 = blueLevelPoints;
			}
			
			Application.LoadLevel("levelOverScreen");
			/*
			if(this.yellowLevelPoints > this.blueLevelPoints) {
				Messenger<string>.Broadcast("LevelFinished", "yellow");
			}
			else if(this.blueLevelPoints > this.yellowLevelPoints) {
				Messenger<string>.Broadcast("LevelFinished", "blue");
			}
			else {
				Messenger<string>.Broadcast("LevelFinished", "tie");
			}*/
		}
	}
	
	void OnDisable() {
		// remove event listeners
		Messenger<string,int>.RemoveListener("ScoreChanged", OnScoreChanged);
	}
	
	public int LevelRemainingTime {
		get {
			float elapsedTime = Time.time - curLevelStartTime;
			return (int)(CountDownSeconds - elapsedTime);
		}
	}
	
	public int YellowLevelPoints { get { return this.yellowLevelPoints; } }
	public int BlueLevelPoints { get { return this.blueLevelPoints; } }
	
	void OnScoreChanged(String team, int score)
    {
		if(team == "yellow") {
	        this.yellowLevelPoints = score;
		}
		else if(team == "blue") {
	        this.blueLevelPoints = score;
		}
	}
}

