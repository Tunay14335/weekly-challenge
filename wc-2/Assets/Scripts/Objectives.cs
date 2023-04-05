using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectives : MonoBehaviour
{
    public static Objectives Instance;

    public enum Stage
    {
        NewBestScore = 3,
        ClearCrystalls = 2,
        LoseGame = 1,
        None = 0,
    }

    private Stage mainStage;
    public Stage GetMainObjective => mainStage;

    public Objective<uint> breakStones {get; set;}


    private void Awake()
    {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
    }

    public void ReloadCurrentStageObjective(Stage stage)
    {
        mainStage = stage > mainStage ? stage : mainStage;
    }
    public void ClearCurrentStageObjective()
    {
        mainStage = Stage.None;
    }

}
