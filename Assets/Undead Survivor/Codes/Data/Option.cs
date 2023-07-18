using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Option
{
    public float masterVolume = 100;
    public float bgmVolume = 50;
    public float seVolume = 50;
    public bool isVisibleJoystick = true;
    public bool isInfinityMode = false;


    public Option(float _masterVolume, float _bgmVolume, float _seVolume, bool _isVisibleJoystick, bool _isInfinityMode)
    {
        masterVolume = _masterVolume;
        bgmVolume = _bgmVolume;
        seVolume = _seVolume;
        isVisibleJoystick = _isVisibleJoystick;
        isInfinityMode = false;
    }

    public Option()
    {
        masterVolume = 100.0f;
        bgmVolume = 100.0f;
        seVolume = 100.0f;
        isVisibleJoystick = false;
        isInfinityMode = false;
    }

}
