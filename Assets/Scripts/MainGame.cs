using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainGame
{
	private static MainGame instance;
	
	private int yellowTotalWins = 0;
	private int blueTotalWins = 0;
	
	private string[] levels = {"farmLevel","lakeLevel","spaceLevel"};
	private int currentLevel = 0;
 
	public MainGame() {
		if (instance == null) {
			instance = this;
			
			Messenger<string>.AddListener("LevelFinished", OnLevelFinished);
			Messenger.AddListener("AdvanceLevel", OnAdvanceLevel);
			MonoBehaviour.print("Just added advancelevel listener");
			
			YellowLevel1 = -1;
			YellowLevel2 = -1;
			YellowLevel3 = -1;
			BlueLevel1 = -1;
			BlueLevel2 = -1;
			BlueLevel3 = -1;
		}
	}

	public static MainGame Instance	{
		get {
			if (instance == null) new MainGame();
			return instance;
		}
	}
	
	public int YellowLevel1 { get; set;}
	public int YellowLevel2 { get; set;}
	public int YellowLevel3 { get; set;}
	public int BlueLevel1 { get; set;}
	public int BlueLevel2 { get; set;}
	public int BlueLevel3 { get; set;}
	
	public void StartGame() {
		StartNewLevel(levels[0]);
		yellowTotalWins = 0;
		blueTotalWins = 0;
	}
	
	private void StartNewLevel(string levelName) {
		Application.LoadLevel(levelName);
	}
	
	public void ShowMainMenu() {
		Application.LoadLevel("MainMenuScreen");
	}
	
	public void ShowInstructions() {
		Application.LoadLevel("InstructionsScreen");
	}
	
	void OnLevelFinished(string team) {
        if(team == "yellow") this.yellowTotalWins++;
		else if(team == "blue") this.blueTotalWins++;
	}
	
	void OnAdvanceLevel() {
		if(currentLevel >= 2) {
			currentLevel = 0;
			
			YellowLevel1 = -1;
			YellowLevel2 = -1;
			YellowLevel3 = -1;
			BlueLevel1 = -1;
			BlueLevel2 = -1;
			BlueLevel3 = -1;
			ShowMainMenu();
			return;
		}
		MonoBehaviour.print("currentLevel before: "+currentLevel.ToString());
		currentLevel++;
		MonoBehaviour.print("currentLevel after: "+currentLevel.ToString());
		StartNewLevel(levels[currentLevel]);
	}
}
