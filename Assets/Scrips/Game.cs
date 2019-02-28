using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Game : MonoBehaviour {

	State gameState = null;

	LevelController level = null;

	MenuController menu = null;

	// Use this for initialization
	void Start () {

		InitGameState();

		menu = GetComponentInChildren<MenuController>(true);
		level = GetComponentInChildren<LevelController>(true);

		menu.gameObject.SetActive(true);
		level.gameObject.SetActive(false);

		if (gameState != null){
			menu.SetScores(null, gameState, -1);
		}
		else{
			// show welcome message
			Debug.Log("No state yet");
		}
		
	}
	
	void InitGameState(){
		gameState = State.Load();

		if (gameState == null){
			gameState = new State();
		}
	}

	public void StartLevel(){
		menu.gameObject.SetActive(false);
		level.gameObject.SetActive(true);
		level.StartLevel();
	}

	void EndLevel(int newScore){
		
		menu.gameObject.SetActive(true);
		level.gameObject.SetActive(false);

		var ledderboardIndex = UpdateLadderboardAndGetCurrentScoreIndex(newScore);

		menu.SetScores(newScore, gameState, ledderboardIndex);
	}

	int UpdateLadderboardAndGetCurrentScoreIndex(int newScore){

		var newGameState = new State();

		// insert all the scores that are greater than the new score
		for (int i = 0; i < gameState.HighScores.Count; i++){
			if (gameState.HighScores[i].Score > newScore){
				newGameState.HighScores.Add(gameState.HighScores[i]);
			}
		}

		var newScoreIndex = -1;

		// got into top 10?
		if (newGameState.HighScores.Count < 10){
			// Add the current one
			newScoreIndex = newGameState.HighScores.Count;
			newGameState.HighScores.Add(new HighScore() {Score = newScore, Time = DateTime.Now});

			// insert all the scores that are below than the new score
			for (int i = 0; i < gameState.HighScores.Count; i++){
				if (gameState.HighScores[i].Score <= newScore){
					if (newGameState.HighScores.Count < 10){
						newGameState.HighScores.Add(gameState.HighScores[i]);
					}
				}
			}
		}

		State.Save(newGameState);
		gameState = State.Load();

		return newScoreIndex;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void SaveState(){
		State.Save(gameState);
	}

	void LoadState(){
		
	}
}
