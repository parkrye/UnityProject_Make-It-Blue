using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class OptionView : View
{
    public override async UniTask OnInit()
    {
        await base.OnInit();

        if (GetSlider("MasterSlider", out var masterSlider))
            masterSlider.value = StaticValues.MasterVolume;
        else
            Debug.Log(gameObject.name + " lost MasterSlider");

        if (GetSlider("BGMSlider", out var bgmSlider))
            bgmSlider.value = StaticValues.BGMVolume;
        else
            Debug.Log(gameObject.name + " lost BGMSlider");

        if (GetSlider("SFXSlider", out var sfxSlider))
            sfxSlider.value = StaticValues.SFXVolume;
        else
            Debug.Log(gameObject.name + " lost SFXSlider");

        if (GetSlider("PostExposureSlider", out var postExposureSlider))
            postExposureSlider.value = StaticValues.PostExposure;
        else
            Debug.Log(gameObject.name + " lost PostExposureSlider");

        if (GetToggle("VSyncToggle", out var vsyncToggle))
            vsyncToggle.isOn = StaticValues.VSync;
        else
            Debug.Log(gameObject.name + " lost VSyncToggle");

        if (GetToggle("MotionBlurToggle", out var motionBlurToggle))
            motionBlurToggle.isOn = StaticValues.MotionBlur;
        else
            Debug.Log(gameObject.name + " lost MotionBlurToggle");

        if (GetToggle("CrossHeadToggle", out var crossheadToggle))
            crossheadToggle.isOn = StaticValues.CrossHead;
        else
            Debug.Log(gameObject.name + " lost CrossHeadToggle");

        if (GetDropDown("GameFrameDropdown", out var gameFrameDropdown))
            gameFrameDropdown.value = (int)StaticValues.GameFrame;
        else
            Debug.Log(gameObject.name + " lost GameFrameDropdown");

        if (GetButton("SaveButton", out var saveButton))
            saveButton.OnClick.AddListener(OnSaveButtonClick);
        else
            Debug.Log(gameObject.name + " lost SaveButton");

        if (GetButton("CancelButton", out var cancelButton))
            cancelButton.OnClick.AddListener(OnCancelButtonClick);
        else
            Debug.Log(gameObject.name + " lost CancelButton");

        if (GetButton("ResetButton", out var resetButton))
            resetButton.OnClick.AddListener(OnResetButtonClick);
        else
            Debug.Log(gameObject.name + " lost ResetButton");
    }

    public override void OnOpenView()
    {
        base.OnOpenView();

        if (GetSlider("MasterSlider", out var masterSlider))
            masterSlider.value = StaticValues.MasterVolume;

        if (GetSlider("BGMSlider", out var bgmSlider))
            bgmSlider.value = StaticValues.BGMVolume;

        if (GetSlider("SFXSlider", out var sfxSlider))
            sfxSlider.value = StaticValues.SFXVolume;

        if (GetSlider("PostExposureSlider", out var postExposure))
            postExposure.value = StaticValues.PostExposure;

        if (GetToggle("VSyncToggle", out var vsyncToggle))
            vsyncToggle.isOn = StaticValues.VSync;

        if (GetToggle("MotionBlurToggle", out var motionBlurToggle))
            motionBlurToggle.isOn = StaticValues.MotionBlur;

        if (GetToggle("CrossheadToggle", out var crossheadToggle))
            crossheadToggle.isOn = StaticValues.CrossHead;

        if (GetDropDown("GameFrameDropdown", out var gameFrameDropdown))
            gameFrameDropdown.value = (int)StaticValues.GameFrame;
    }

    public override void OnCloseView()
    {
        base.OnCloseView();

        if (GetContent("Scroll View", out var scrollRect))
            scrollRect.GetComponent<ScrollRect>().normalizedPosition = Vector2.one;
    }

    private void OnSaveButtonClick()
    {
        if (GetSlider("MasterSlider", out var masterSlider))
            StaticValues.MasterVolume = masterSlider.value;

        if (GetSlider("BGMSlider", out var bgmSlider))
            StaticValues.BGMVolume = bgmSlider.value;

        if (GetSlider("SFXSlider", out var sfxSlider))
            StaticValues.SFXVolume = sfxSlider.value;

        if (GetSlider("PostExposureSlider", out var postExposure))
            StaticValues.PostExposure = postExposure.value;

        if (GetToggle("VSyncToggle", out var vsyncToggle))
            StaticValues.VSync = vsyncToggle.isOn;

        if (GetToggle("MotionBlurToggle", out var motionBlurToggle))
            StaticValues.MotionBlur = motionBlurToggle.isOn;

        if (GetToggle("CrossHeadToggle", out var crossheadToggle))
            StaticValues.CrossHead = crossheadToggle.isOn;

        if (GetDropDown("GameFrameDropdown", out var gameFrameDropdown))
            StaticValues.GameFrame = (GameFrameEnum)gameFrameDropdown.value;
    }

    private void OnCancelButtonClick()
    {
        GameManager.UI.CloseCurrentDialog();
    }

    private void OnResetButtonClick()
    {
        PlayerPrefs.DeleteKey("MasterVolume");
        if (GetSlider("MasterSlider", out var masterSlider))
            masterSlider.value = StaticValues.MasterVolume;

        PlayerPrefs.DeleteKey("BGMVolume");
        if (GetSlider("BGMSlider", out var bgmSlider))
            bgmSlider.value = StaticValues.BGMVolume;

        PlayerPrefs.DeleteKey("SFXVolume");
        if (GetSlider("SFXSlider", out var sfxSlider))
            sfxSlider.value = StaticValues.SFXVolume;

        PlayerPrefs.DeleteKey("PostExposure");
        if (GetSlider("PostExposureSlider", out var postExposure))
            postExposure.value = StaticValues.PostExposure;

        PlayerPrefs.DeleteKey("VSync");
        if (GetToggle("VSyncToggle", out var vsyncToggle))
            vsyncToggle.isOn = StaticValues.VSync;

        StaticValues.MotionBlur = true;
        if (GetToggle("MotionBlurToggle", out var motionBlurToggle))
            motionBlurToggle.isOn = StaticValues.MotionBlur;

        StaticValues.CrossHead = true;
        if (GetToggle("CrossheadToggle", out var crossheadToggle))
            crossheadToggle.isOn = StaticValues.CrossHead;

        PlayerPrefs.DeleteKey("GameFrame");
        if (GetDropDown("GameFrameDropdown", out var gameFrameDropdown))
            gameFrameDropdown.value = (int)StaticValues.GameFrame;
    }
}
