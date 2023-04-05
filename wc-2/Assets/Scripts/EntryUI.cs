using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntryUI : MonoBehaviour
{
    [SerializeField] private Button BTN_PLAY;
    [SerializeField] private Button BTN_ABOUT;

    #region Unity Impls

    private void Awake()
    {
        BTN_PLAY.onClick.AddListener(() => SceneManager.LoadScene(GameLiterals.SCENE_GAME));
    }
    
    #endregion
}
