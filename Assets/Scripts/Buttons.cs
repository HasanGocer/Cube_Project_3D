using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoSingleton<Buttons>
{
    //managerde bulunacak

    [SerializeField] private GameObject _money;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private Button _startButton;

    [SerializeField] private Button _settingButton;
    [SerializeField] private GameObject _settingGame;

    [SerializeField] private Sprite _red, _green;
    [SerializeField] private Button _settingBackButton;
    [SerializeField] private Button _soundButton, _vibrationButton;

    public GameObject winPanel, failPanel, taskPanel;
    [SerializeField] private Button _winPrizeButton, _winButton, _failButton;
    [SerializeField] private GameObject _tutorialPanel;

    public Text moneyText, timerText, levelText;

    private void Start()
    {
        ButtonPlacement();
        SettingPlacement();
        levelText.text = GameManager.Instance.level.ToString();
    }

    public IEnumerator NoThanxSetActive()
    {
        yield return new WaitForSeconds(2);
        _winButton.gameObject.SetActive(true);
    }

    private void SettingPlacement()
    {
        if (GameManager.Instance.sound == 1)
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            // SoundSystem.Instance.MainMusicPlay();
        }
        else
        {
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
        }

        if (GameManager.Instance.vibration == 1)
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
        }
        else
        {
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
        }
    }
    private void ButtonPlacement()
    {
        _startButton.onClick.AddListener(() => StartCoroutine(StartButton()));
        _settingButton.onClick.AddListener(SettingButton);
        _settingBackButton.onClick.AddListener(SettingBackButton);
        _soundButton.onClick.AddListener(SoundButton);
        _vibrationButton.onClick.AddListener(VibrationButton);
        _winButton.onClick.AddListener(() => StartCoroutine(WinButton()));
        _winPrizeButton.onClick.AddListener(() => StartCoroutine(WinPrizeButton()));
        _failButton.onClick.AddListener(FailButton);
    }


    private IEnumerator StartButton()
    {
        _tutorialPanel.SetActive(true);
        _startPanel.SetActive(false);
        ViewTaskSystem.Instance.ViewPanelOn();
        TaskSystem.Instance.TaskStart();
        ViewTaskSystem.Instance.OpenQuestionMark();
        ScaleSystem.Instance.startScaleSystem();
        CabinetSystem.Instance.StartCabinetSystem();
        yield return new WaitForSeconds(6);
        StartCoroutine(ViewTaskSystem.Instance.WievTaskOff());
        _tutorialPanel.SetActive(false);
        taskPanel.SetActive(true);
        GameManager.Instance.isStart = true;
        StartCoroutine(OpenLight.Instance.LightIsThere());
        StartCoroutine(TimerSystem.Instance.TimerStart());
    }
    private IEnumerator WinPrizeButton()
    {
        GameManager.Instance.level++;
        GameManager.Instance.SetLevel();
        LevelSystem.Instance.NewLevelCheckField();
        BarSystem.Instance.BarStopButton(GameManager.Instance.addedMoney);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
    private IEnumerator WinButton()
    {
        GameManager.Instance.level++;
        GameManager.Instance.SetLevel();
        LevelSystem.Instance.NewLevelCheckField();
        BarSystem.Instance.BarStopButton(0);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
    private void FailButton()
    {
        SceneManager.LoadScene(0);
    }
    private void SettingButton()
    {
        _settingGame.SetActive(true);
        _settingButton.gameObject.SetActive(false);
        _money.SetActive(false);
    }
    private void SettingBackButton()
    {
        _settingGame.SetActive(false);
        _settingButton.gameObject.SetActive(true);
        _money.SetActive(true);
    }
    private void SoundButton()
    {
        if (GameManager.Instance.sound == 1)
        {
            GameManager.Instance.sound = 0;
            _soundButton.gameObject.GetComponent<Image>().sprite = _red;
            SoundSystem.Instance.MainMusicStop();
            GameManager.Instance.sound = 0;
            GameManager.Instance.SetSound();
        }
        else
        {
            GameManager.Instance.sound = 1;
            _soundButton.gameObject.GetComponent<Image>().sprite = _green;
            SoundSystem.Instance.MainMusicPlay();
            GameManager.Instance.sound = 1;
            GameManager.Instance.SetSound();
        }
    }
    private void VibrationButton()
    {
        if (GameManager.Instance.vibration == 1)
        {
            GameManager.Instance.vibration = 0;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _red;
            GameManager.Instance.vibration = 0;
            GameManager.Instance.SetVibration();
        }
        else
        {
            GameManager.Instance.vibration = 1;
            _vibrationButton.gameObject.GetComponent<Image>().sprite = _green;
            GameManager.Instance.vibration = 1;
            GameManager.Instance.SetVibration();
        }
    }
}
