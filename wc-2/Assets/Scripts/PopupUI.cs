using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PopupUI : MonoBehaviour
{
    public static PopupUI Instance;

    [SerializeField] private GameObject POPUP_PANEL;
    [SerializeField] private GameObject GROUP_SCOREUI;
    [SerializeField] private TextMeshProUGUI TEXT_INFO;
    [SerializeField] private TextMeshProUGUI TEXT_SCORE;
    [SerializeField] private TextMeshProUGUI TEXT_BESTSCORE;
    [SerializeField] private TextMeshProUGUI TEXT_BESTSCORE_PAUSED;
    [SerializeField] private Button BTN_PAUSE;
    [SerializeField] private Button BTN_RESUME;
    [SerializeField] private Button BTN_REPLAY;
    [SerializeField] private Button BTN_EXIT;

    private readonly string LABEL_PAUSE = "Paused";
    private readonly string LABEL_BESTSCORE = "Best";
    private readonly string LABEL_NEWBESTSCORE = "New Best Score";
    private readonly string LABEL_LOSE = "Game Over";
    private readonly string LABEL_COMPLETE = "Crystals All Cleared";

    public UnityAction OnResume;
    public UnityAction OnReplay;
    public UnityAction OnExit;
    public UnityAction OnPause;

    #region Unity Impls

    private void Awake()
    {
        if(Instance != null)
            Destroy(this);
        else
            Instance = this;
    }
    
    #endregion

    #region Custom Impls

    public void LoadAllEvents()
    {
        BTN_PAUSE.onClick.AddListener(OnPause);
        BTN_RESUME.onClick.AddListener(OnResume);
        BTN_REPLAY.onClick.AddListener(OnReplay);
        BTN_EXIT.onClick.AddListener(OnExit);
    }

    public void SetPopupPanel(bool state)
    {
        POPUP_PANEL.SetActive(state);
    }

    public void ShowPauseMenu()
    {
        TEXT_INFO.text = LABEL_PAUSE;
        TEXT_BESTSCORE_PAUSED.text = LABEL_BESTSCORE +" : "+ ScoreSystem.Instance.BestScore.ToString();
        BTN_RESUME.gameObject.SetActive(true);
        BTN_REPLAY.gameObject.SetActive(true);
        BTN_EXIT.gameObject.SetActive(true);
        GROUP_SCOREUI.SetActive(false);
        POPUP_PANEL.SetActive(true);
    }
    public void ShowNewBestScoreMenu()
    {
        TEXT_INFO.text = LABEL_NEWBESTSCORE;
        TEXT_SCORE.text = ScoreSystem.Instance.Score.ToString();
        TEXT_BESTSCORE.text = LABEL_BESTSCORE +" : "+ ScoreSystem.Instance.BestScore.ToString();
        BTN_RESUME.gameObject.SetActive(false);
        BTN_REPLAY.gameObject.SetActive(true);
        BTN_EXIT.gameObject.SetActive(true);
        GROUP_SCOREUI.SetActive(true);
        POPUP_PANEL.SetActive(true);
    }
    public void ShowLoseGameMenu()
    {
        TEXT_INFO.text = LABEL_LOSE;
        TEXT_SCORE.text = ScoreSystem.Instance.Score.ToString();
        TEXT_BESTSCORE.text = LABEL_BESTSCORE +" : "+ ScoreSystem.Instance.BestScore.ToString();
        BTN_RESUME.gameObject.SetActive(false);
        BTN_REPLAY.gameObject.SetActive(true);
        BTN_EXIT.gameObject.SetActive(true);
        GROUP_SCOREUI.SetActive(true);
        POPUP_PANEL.SetActive(true);
    }
    public void ShowCompleteGameMenu()
    {
        TEXT_INFO.text = LABEL_COMPLETE;
        TEXT_SCORE.text = ScoreSystem.Instance.Score.ToString();
        TEXT_BESTSCORE.text = LABEL_BESTSCORE +" : "+ ScoreSystem.Instance.BestScore.ToString();
        BTN_RESUME.gameObject.SetActive(false);
        BTN_REPLAY.gameObject.SetActive(true);
        BTN_EXIT.gameObject.SetActive(true);
        GROUP_SCOREUI.SetActive(true);
        POPUP_PANEL.SetActive(true);
    }
    
    #endregion
}
