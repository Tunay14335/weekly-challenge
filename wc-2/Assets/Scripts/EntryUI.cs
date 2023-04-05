using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EntryUI : MonoBehaviour
{
    [SerializeField] private GameObject POPUP_PANEL;
    [SerializeField] private Button BTN_PLAY;
    [SerializeField] private Button BTN_ABOUT;
    [SerializeField] private Button BTN_ABOUT_BACK;
    [SerializeField] private Button BTN_LINK_SOURCE_CODE;

    #region Unity Impls

    private void Awake()
    {
        BTN_PLAY.onClick.AddListener(() => SceneManager.LoadScene(GameLiterals.SCENE_GAME));
        BTN_ABOUT.onClick.AddListener(() => SetPopupPanel(true));
        BTN_ABOUT_BACK.onClick.AddListener(() => SetPopupPanel(false));
        BTN_LINK_SOURCE_CODE.onClick.AddListener(() => Application.OpenURL("https://github.com/Tunay14335/weekly-challenge/tree/main/wc-2"));
    }
    
    #endregion

    #region Custom Impls

    private void SetPopupPanel(bool state)
    {
        POPUP_PANEL.SetActive(state);
    }

    #endregion
}
