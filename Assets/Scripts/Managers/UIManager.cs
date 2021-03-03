using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UI Manager is null.");
            return _instance;
        }
    }

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenuCanvas;
    [SerializeField] private GameObject _levelSelectionCanvas;
    [SerializeField] private GameObject _inGameCanvas;
    [SerializeField] private GameObject _creditsMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _finishCanvas;

    [Header("Buttons")]
    [SerializeField] private GameObject _resumeButton;

    [Header("Sliders")]
    [SerializeField] private Slider _mainSoundSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    [SerializeField] private List<GameObject> levelButtons;

    int keyCard = 0;
    bool changeValue = false;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        if(keyCard == levelButtons.Count)
        {
            LoadFinishCanvas();
            keyCard++;
        }
        if(changeValue == true)
        {
            changeValue = false;
            if(keyCard <= levelButtons.Count)
                levelButtons[keyCard].GetComponent<Button>().interactable = true;
            else return;
        }

        ChangeMusicValueBySlider();
        ChangeSFXValueBySlider();

    }

    public void LoadLevelSelectionScene()
    {
        Time.timeScale = 1;
        _levelSelectionCanvas.SetActive(!_levelSelectionCanvas.activeSelf);
    }

    public void OptionsMenu()
    {
        _optionsMenu.SetActive(!_optionsMenu.activeSelf);
    }

    public void LoadMainMenu()
    {
        if(_finishCanvas.activeSelf)
            _finishCanvas.SetActive(false);
        Time.timeScale = 1;
        _mainMenuCanvas.SetActive(!_mainMenuCanvas.activeSelf);
    }

    public void LoadPauseMenu()
    {
        if(Time.timeScale == 1)
            Time.timeScale = 0;
        else Time.timeScale = 1;
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);
    }

    public void PlayButton()
    {
        LoadLevelSelectionScene();
        LoadMainMenu();
    }

    public void CreditsButton()
    {
        _creditsMenu.SetActive(!_creditsMenu.activeSelf);
    }

    public void AddOneKeyCard()
    {
        keyCard++;
        changeValue = true;
    }


    public void ChangeMusicValueBySlider()
    {
        MusicChanger.Instance.GetAudioSource().volume = _musicSlider.value;
    }

    public void ChangeSFXValueBySlider()
    {
        SFXChanger.Instance.GetAudioSource().volume = _SFXSlider.value;
    }

    public void ChangeListenerValueBySlider()
    {
        AudioListener.volume = _mainSoundSlider.value;
    }

    public void LoadFinishCanvas()
    {
        _finishCanvas.SetActive(!_finishCanvas.activeSelf);
    }

}
