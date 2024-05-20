using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    private AudioMixer _audioMixer;

    private void Awake()
    {
        _audioMixer = GameManager.Resource.Load<AudioMixer>("Audio/AudioMixer");
    }

    private void Start()
    {
        GameManager.System.AddValueTrackAction(ValueTrackEvent);
    }

    public void ValueTrackEvent(ValueTrackEnum valueEnum)
    {
        switch(valueEnum)
        {
            case ValueTrackEnum.MasterVolume:
                MasterVolume = StaticValues.MasterVolume;
                break;
            case ValueTrackEnum.BGMVolume:
                BGMVolume = StaticValues.BGMVolume;
                break;
            case ValueTrackEnum.SFXVolume:
                SFXVolume = StaticValues.SFXVolume;
                break;
            default:
                break;
        }
    }

    private float MasterVolume
    {
        set
        {
            if (value == 0f)
            {
                _audioMixer.SetFloat("Master", -80f);
            }
            else
            {
                if(value == 0f)
                    _audioMixer.SetFloat("Master", -80f);
                else
                    _audioMixer.SetFloat("Master", -20f + value * 20f);
            }
        }
    }

    private float BGMVolume
    {
        set
        {
            if (value == 0f)
            {
                _audioMixer.SetFloat("BGM", -80f);
            }
            else
            {
                if (value == 0f)
                    _audioMixer.SetFloat("BGM", -80f);
                else
                    _audioMixer.SetFloat("BGM", -20f + value * 20f);
            }
        }
    }

    private float SFXVolume
    {
        set
        {
            if (value == 0f)
            {
                _audioMixer.SetFloat("SFX", -80f);
            }
            else
            {
                if (value == 0f)
                    _audioMixer.SetFloat("SFX", -80f);
                else
                    _audioMixer.SetFloat("SFX", -20f + value * 20f);
            }
        }
    }
}