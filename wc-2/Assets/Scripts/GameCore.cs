using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameCore : MonoBehaviour , IGameInstruction
{
    public static GameCore Instance;
    public static GameState gameState;

    public enum GameState
    {
        end = 0,
        play = 1,
        pause = 2,
    }

    [SerializeField] private Cannon cannon;

    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private ObjectPool stonePool;

    [SerializeField] private Sprite[] crystalSprites;

    public ObjectPool GetBulletPool => bulletPool;
    public ObjectPool GetStonePool => stonePool;

    public Sprite[] GetCrystalSprites => crystalSprites;

    private UnityAction OnGameBegin;
    private UnityAction OnGameEnd;

    private IGameInstruction GameInstruction => this as IGameInstruction;

    #region Unity Impls

    private void Awake()
    {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;

        OnGameBegin += () => {
            gameState = GameState.play;
            Time.timeScale = 1f;
        };

        OnGameEnd += () => {
            gameState = GameState.end;
            Time.timeScale = 0f;
        };

        PopupUI.Instance.OnResume += () => {
            PopupUI.Instance.SetPopupPanel(false);
            gameState = GameState.play;
            Time.timeScale = 1f;
        };
        PopupUI.Instance.OnReplay += () => {
            PopupUI.Instance.SetPopupPanel(false);
            GameInstruction.Clear();
            GameInstruction.Init();
            Time.timeScale = 1f;
        };
        PopupUI.Instance.OnPause += () => {
            gameState = GameState.pause;
            PopupUI.Instance.ShowPauseMenu();
            Time.timeScale = 0f;
        };
        PopupUI.Instance.OnExit += () => {
            UnityEngine.SceneManagement.SceneManager.LoadScene(GameLiterals.SCENE_ENTRY);
        };

        PopupUI.Instance.LoadAllEvents();
        GameInstruction.Setup();
    }

    private void Start() => GameInstruction.Init();

    private void Update()
    {
        #if UNITY_STANDALONE_WIN
            if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        #endif

        if(gameState == GameState.play)
        {
            bool r_input = Input.GetKey(KeyCode.RightArrow);
            bool l_input = Input.GetKey(KeyCode.LeftArrow);

            if(r_input) cannon.Move(Vector2.right);
            if(l_input) cannon.Move(Vector2.left);

            if(!(r_input || l_input)) cannon.Move(Vector2.zero);
        }
    }
    
    #endregion

    #region GameInstruction Impls

    void IGameInstruction.Setup()
    {
        bulletPool.Allocate(GameLiterals.INITAL_BULLET_COUNT);
        Debug.Log(bulletPool.Count);

        uint maxAllocStoneCount = (uint)Mathf.Pow(2, StoneLiterals.Scale.Max - StoneLiterals.Scale.Min);

        stonePool.Allocate(maxAllocStoneCount);
        Debug.Log(stonePool.Count);
    }

    void IGameInstruction.Clear()
    {
        {
            Transform poolParent = bulletPool.content.poolParent;
            for(int i = 0; i < poolParent.childCount; i++)
            {
                GameObject obj = poolParent.GetChild(i).gameObject;
                if(obj.activeInHierarchy)
                {
                    bulletPool.Enqueue(obj);
                    obj.SetActive(false);   
                }
            }
        }
        {
            Transform poolParent = stonePool.content.poolParent;
            for(int i = 0; i < poolParent.childCount; i++)
            {
                GameObject obj = poolParent.GetChild(i).gameObject;
                if(obj.activeInHierarchy)
                {
                    stonePool.Enqueue(obj);
                    obj.SetActive(false);   
                }
            }
        }
        Objectives.Instance.ClearCurrentStageObjective();
    }

    void IGameInstruction.Init()
    {
        uint rootScale = (uint)Random.Range((int)StoneLiterals.Scale.Min, (int)StoneLiterals.Scale.Max +1);
        uint segmentationStep = rootScale-StoneLiterals.Scale.Min;
        uint totalStoneCount = (uint)Mathf.Pow(2, segmentationStep + 1)-1;

        GameObject rootStoneObj = stonePool.Dequeue() as GameObject;
        Stone rootStone = rootStoneObj.GetComponent<Stone>();
        
        uint randCount = (uint)Random.Range(
            GameLiterals.INITIAL_STONE_COUNT_INTERVAL.Min,
            GameLiterals.INITIAL_STONE_COUNT_INTERVAL.Max
        );

        rootStone.Scale = rootScale;
        rootStone.Count = randCount;
        rootStone.InitVelocity = GameLiterals.INITAL_ROOT_STONE_VELOCITY;

        Objectives.Instance.breakStones = new Objective<uint>(totalStoneCount);
        Objectives.Instance.breakStones.OnGoalAchieved += () =>{
            Objectives.Instance.ReloadCurrentStageObjective(Objectives.Stage.ClearCrystalls);
            RequestEndGame();
            Debug.Log("Game end");
        };

        ScoreSystem.Instance.ResetScore();
        ScoreSystem.Instance.ReloadBestScore();

        rootStone.gameObject.SetActive(true);
        cannon.transform.position *= Vector2.up;

        OnGameBegin.Invoke();
    }

    void IGameInstruction.End()
    {
        OnGameEnd.Invoke();
        bool isNewScore = ScoreSystem.Instance.ReloadBestScore();

        if(isNewScore) Objectives.Instance.ReloadCurrentStageObjective(Objectives.Stage.NewBestScore);

        switch(Objectives.Instance.GetMainObjective)
        {
            case Objectives.Stage.None:
                break;
            case Objectives.Stage.LoseGame:
                PopupUI.Instance.ShowLoseGameMenu();
                break;
            case Objectives.Stage.ClearCrystalls:
                PopupUI.Instance.ShowCompleteGameMenu();
                break;
            case Objectives.Stage.NewBestScore:
                PopupUI.Instance.ShowNewBestScoreMenu();
                break;
        }
    }

    #endregion

    #region Custom Impls
    
    public void RequestEndGame() => GameInstruction.End();

    #endregion
}
