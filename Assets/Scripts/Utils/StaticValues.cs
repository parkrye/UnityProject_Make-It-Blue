using System.Text;
using UnityEngine;

public enum GameFrameEnum
{
    Thirty = 0,
    Sixty,
    Ninety,
    Hundread
}

public enum ValueTrackEnum
{
    MasterVolume,
    BGMVolume,
    SFXVolume,
    PostExposure,
    MotionBlur,
    VSync,
    GameFrame,
    CrossHead,

    Minitues,
}

public static class StaticValues
{
    public static int DefaultHP = 100;
    public static int DefaultSP = 100;
    public static float DefaultMoveSpeed = 3f;
    public static float DefaultTurnSpeed = 60f;
    public static int DefaultStartStatusPoint = 4;

    public static int DefaultLimitLevel = 10;

    public static float MasterVolume
    {
        get
        {
            var volume = -10f;
            if (PlayerPrefs.HasKey("MasterVolume"))
                volume = PlayerPrefs.GetFloat("MasterVolume");
            else
                PlayerPrefs.SetFloat("MasterVolume", volume);
            return (volume + 20f) * 0.05f;
        }
        set
        {
            PlayerPrefs.SetFloat("MasterVolume", Mathf.Clamp(value * 20f - 20f, -20f, 0f));
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.MasterVolume);
        }
    }

    public static float BGMVolume
    {
        get
        {
            var volume = -10f;
            if (PlayerPrefs.HasKey("BGMVolume"))
                volume = PlayerPrefs.GetFloat("BGMVolume");
            else
                PlayerPrefs.SetFloat("BGMVolume", volume);
            return (volume + 20f) * 0.05f;
        }
        set
        {
            PlayerPrefs.SetFloat("BGMVolume", Mathf.Clamp(value * 20f - 20f, -20f, 0f));
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.BGMVolume);
        }
    }

    public static float SFXVolume
    {
        get
        {
            var volume = -10f;
            if (PlayerPrefs.HasKey("SFXVolume"))
                volume = PlayerPrefs.GetFloat("SFXVolume");
            else
                PlayerPrefs.SetFloat("SFXVolume", volume);
            return (volume + 20f) * 0.05f;
        }
        set
        {
            PlayerPrefs.SetFloat("SFXVolume", Mathf.Clamp(value * 20f - 20f, -20f, 0f));
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.SFXVolume);
        }
    }

    public static float PostExposure
    {
        get
        {
            var postExposure = 0f;
            if (PlayerPrefs.HasKey("PostExposure"))
                postExposure = PlayerPrefs.GetFloat("PostExposure");
            else
                PlayerPrefs.SetFloat("PostExposure", postExposure);
            return postExposure;
        }
        set
        {
            PlayerPrefs.SetFloat("PostExposure", Mathf.Clamp(value, -1f, 1f));
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.PostExposure);
        }
    }

    public static bool VSync
    {
        get
        {
            var isOn = true;
            if (PlayerPrefs.HasKey("VSync"))
                isOn = PlayerPrefs.GetInt("VSync") == 1;
            else
                PlayerPrefs.SetInt("VSync", 1);
            return isOn;
        }
        set
        {
            var vsync = value ? 1 : 0;
            PlayerPrefs.SetInt("VSync", vsync);
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.VSync);
        }
    }

    public static GameFrameEnum GameFrame
    {
        get
        {
            var gameFrame = GameFrameEnum.Sixty;
            if (PlayerPrefs.HasKey("GameFrame"))
                gameFrame = (GameFrameEnum)PlayerPrefs.GetInt("GameFrame");
            else
                PlayerPrefs.SetInt("GameFrame", (int)gameFrame);
            return gameFrame;
        }
        set
        {
            PlayerPrefs.SetInt("GameFrame", Mathf.Clamp((int)value, 0, 3));
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.GameFrame);
            Application.targetFrameRate = (int)(value + 1) * 30;
        }
    }

    public static bool MotionBlur
    {
        get
        {
            var isOn = true;
            if (PlayerPrefs.HasKey("MotionBlur"))
                isOn = PlayerPrefs.GetInt("MotionBlur") == 1;
            else
                PlayerPrefs.SetInt("MotionBlur", 1);
            return isOn;
        }
        set
        {
            PlayerPrefs.SetInt("MotionBlur", value ? 1 : 0);
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.MotionBlur);
        }
    }

    public static bool FilmGrain { get; set; } = false;
    public static bool Vignette { get; set; } = false;
    public static bool Bloom { get; set; } = false;
    public static bool CrossHead
    {
        get
        {
            var isOn = true;
            if (PlayerPrefs.HasKey("CrossHead"))
                isOn = PlayerPrefs.GetInt("CrossHead") == 1;
            else
                PlayerPrefs.SetInt("CrossHead", 1);
            return isOn;
        }
        set
        {
            PlayerPrefs.SetInt("CrossHead", value ? 1 : 0);
            GameManager.System.TriggerValueTrackEvent(ValueTrackEnum.CrossHead);
        }
    }

    public static float TextDuration
    {
        get
        {
            var duration = 1f;
            if (PlayerPrefs.HasKey("TextDuration"))
                duration = PlayerPrefs.GetFloat("TextDuration");
            else
                PlayerPrefs.SetFloat("TextDuration", duration);
            return duration;
        }
        set
        {
            PlayerPrefs.SetFloat("TextDuration", value);
        }
    }

    public static string GetContext(string before)
    {
        var sb = new StringBuilder();
        var split = before.Split(' ');

        foreach (var item in split)
        {
            switch (item)
            {
                default:
                    sb.Append(item);
                    break;
                case "{Player}":
                    sb.Append(GameManager.Data.Play.Name[0]);
                    break;
                case "{Debt}":
                    sb.Append(GameManager.Data.Play.Debt);
                    break;
            }
            sb.Append(" ");
        }

        return sb.ToString();
    }
}
