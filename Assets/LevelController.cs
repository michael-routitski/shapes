using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    const int BLOCKS_PER_LEVEL = 10;

    private LevelUIManager UI = null;

    int CurrentLevel = 1;

    int CurrentBlocksRemaining = 10;

    int BlocksTotal = 10;

    int CurrentScore = 0;

    CameraFollow cameraFollow;

    PlayerController player;

    void Update(){
        if (cameraFollow){
            cameraFollow.RunUpdate();
        }
    }

    public void StartLevel(){
        UI = GetComponentInChildren<LevelUIManager>(true);
        cameraFollow = GetComponentInChildren<CameraFollow>(true);
        player = GetComponentInChildren<PlayerController>(true);

        CurrentScore = 0;
        CurrentLevel = 1;
        CurrentBlocksRemaining = BLOCKS_PER_LEVEL;

        player.Reset();

        UpdateUI();

        GetComponentInChildren<CameraFollow>().Reset();
    }

    public void HandleSugarCubePass() {
        
        CurrentScore++;
        CurrentBlocksRemaining--;
        if (CurrentBlocksRemaining <= 0){
            CurrentLevel++;
            CurrentBlocksRemaining = BLOCKS_PER_LEVEL;
        }

        UpdateUI();
    }

    public void HandleSugarCubeCrash() {
        SendMessageUpwards("EndLevel", CurrentScore, SendMessageOptions.RequireReceiver);
    }
    
    void UpdateUI(){

        UI.SetTotal(BlocksTotal);
        UI.SetLevel(CurrentLevel);
        UI.SetProgress(CurrentBlocksRemaining);
        UI.SetScore(CurrentScore);
    }

}
