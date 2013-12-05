using UnityEngine;
using System;
using System.Collections;

public class GameGUI: MonoBehaviour {
	
	private string debugMsg="";	
	public GUISkin skin;
	public Texture2D icon1;
	public Texture2D icon2;
	GUIStyle bgStyle = new GUIStyle();
	private SpawnDucks BlueScriptName;
	private SpawnDucks YellowScriptName;
	
	void Start()
	{
		BlueScriptName = GameObject.FindWithTag("blueMomDuck").GetComponent<SpawnDucks>();
		YellowScriptName = GameObject.FindWithTag("yellowMomDuck").GetComponent<SpawnDucks>();
	}
	
	void OnEnable() {
		//Messenger<string>.AddListener("LevelFinished", OnLevelFinished);
	}
	
	void OnDisable() {
		//Messenger<string>.RemoveListener("LevelFinished", OnLevelFinished);
	}
	
	void Update(){
		if (Input.GetKey(KeyCode.Escape)) Application.Quit(); // end game when Escape is pressed
	}
	/*
	IEnumerator DisplayWinner(string team) {
		if(team != "tie") {
			print (team+" wins!");
		}
		else {
			print ("It's a tie!");
		}
		yield return new WaitForSeconds(4);
		Messenger.Broadcast("AdvanceLevel");
		print ("Level should advance");
	}

	void OnLevelFinished(string team) {
		StartCoroutine(DisplayWinner(team));
	}
	*/
	void OnGUI(){
		GUI.skin = skin;
		//bgStyle.normal.background = backdrop;
		
		int w_center = (Screen.width/2);
		int h_center = (Screen.height/2);
		int w_double = (2*(Screen.width));
		int h_triple = (3*(Screen.height));
		
		//Background image
		GUI.Box(new Rect(150,5,350,60),new GUIContent(LevelManager.Instance.BlueLevelPoints.ToString(), icon2));
		GUI.Box(new Rect(850,5,350,60),new GUIContent(LevelManager.Instance.YellowLevelPoints.ToString(), icon1));
		
		//display the timer
		int remainingSeconds = LevelManager.Instance.LevelRemainingTime;
	    int displaySeconds = remainingSeconds % 60;
	    int displayMinutes = remainingSeconds / 60;
	    String timeText = String.Format ("{0:00}:{1:00}", displayMinutes, displaySeconds); 
		
		GUI.Box(new Rect(600,5,150,60),new GUIContent(timeText));
	}
}

