using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using System.Runtime.Serialization.Formatters.Binary;    
using System;

[Serializable]
public class State {

	public State(){
		HighScores = new List<HighScore>();
	}

	public List<HighScore> HighScores;

	const string FILE_NAME = "game_state_1";

	private static string FilePath {
		get {
			return Path.Combine(Application.persistentDataPath, FILE_NAME);
		}
	}

	public static void Save(State data) {
		//savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (FilePath);
		bf.Serialize(file, data);
		file.Close();
	}

	public static State Load() {
		var path  = Path.Combine(Application.persistentDataPath, FILE_NAME);
		if (File.Exists (path)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (path, FileMode.Open);
			State gameData = (State)bf.Deserialize (file);
			file.Close ();
			return gameData;
		} else {
			return null;
		}
	}
}

[Serializable]
public class HighScore{
	public DateTime Time;
	public int Score;
}
