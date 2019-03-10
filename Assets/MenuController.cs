using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class MenuController : MonoBehaviour {

    public TextMeshPro[] ScoreTexts; 

    public MeshRenderer[] TileRenderers;

    public Material DefaultMaterial;

    public Material NewScoreMaterial;

    StringBuilder _text;

    void Awake(){
        _text = new StringBuilder();
    }

    public void StartLevel(){
        Debug.Log("Start Level");
        GameObject.FindObjectOfType<Game>().StartLevel();         
    }

    public void SetScores(int? score, State state, int indexOfScoreOnLedderboard){
        
        for (int i = 0; i < 10; i++){

            _text.Remove(0, _text.Length);

            if (state.HighScores.Count - 1 >= i){
                _text
                    .Append(state.HighScores[i].Score)
                    .Append(" (")
                    .Append(state.HighScores[i].Time.ToString("dd/MM/yy"))
                    .Append(")");
            }

            ScoreTexts[i].text = _text.ToString();

            TileRenderers[i].sharedMaterial = DefaultMaterial;
        }

        if (score.HasValue){
            if (indexOfScoreOnLedderboard < 10){
                TileRenderers[indexOfScoreOnLedderboard].sharedMaterial = NewScoreMaterial;
            }
        }
	}
}
