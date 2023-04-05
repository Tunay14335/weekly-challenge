using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;
    public static readonly string PATH = "score";

    [SerializeField] private TextMeshProUGUI scoreLabel;

    private uint m_score;
    
    public uint Score
    {
        get => m_score;
        private set
        {
            m_score = value;
            ReloadScoreLabel();
        }
    }
    public uint BestScore {get; private set;}

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

    public void ResetScore() => Score = 0;
    public void IncreaseScore() => Score++;

    public void ReloadScoreLabel() => scoreLabel.text = Score.ToString();

    private void SaveScore()
    {
        PlayerPrefs.SetString(PATH, Score.ToString());
        Debug.Log(Score);
    }

    //is new score return true,
    public bool ReloadBestScore()
    {
        uint bestScore = uint.Parse(PlayerPrefs.GetString(PATH, "0"));
        if(Score > bestScore)
        {
            SaveScore();
            this.BestScore = Score;
            return true;
        }
        this.BestScore = bestScore;
        return false;
    }

    #endregion

}
