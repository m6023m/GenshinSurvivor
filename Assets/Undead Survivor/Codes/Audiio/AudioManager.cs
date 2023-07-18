using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum BGM
    {
        Main,
        Battle0,
        Wish,
        Battle1,
        Battle2,
        Battle3,
    }

    public enum SFX
    {
        Dead,
        Hit,
        LevelUp = 3,
        Lose,
        Melee,
        Range = 7,
        Select,
        Victory,
        Regen,
        Paimon,
        PaimonBack,
        Heal,
        TimeStop,
        BoxBreak,
        Click,
        Click_Wish,
        WishEffect
    }


    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public int bgmChannels;
    public AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;
    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public int sfxChannels;
    public AudioSource[] sfxPlayers;
    int sfxChannelIndex = 0;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        Init();
    }

    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = new AudioSource();

        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;

        //효곽음 플레이어 초기화
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[sfxChannels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;

        }

        OnChangeVolume();
    }

    public void PlayBGM(BGM bgm, bool isPlay)
    {
        bgmPlayer.clip = bgmClips[(int)bgm];
        bgmPlayer.bypassListenerEffects = true;
        if (isPlay)
        {
            bgmPlayer.Stop();
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isPlay)
    {
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();
        bgmPlayer.bypassListenerEffects = !isPlay;
        bgmEffect.enabled = isPlay;
    }
    public void PlaySFX(SFX sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + sfxChannelIndex) % sfxPlayers.Length;
            if (sfxPlayers[loopIndex].isPlaying) continue;

            int randomIndex = 0;
            if (sfx == SFX.Hit || sfx == SFX.Melee)
            {
                randomIndex = Random.Range(0, 2);
            }

            sfxChannelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + randomIndex];
            sfxPlayers[loopIndex].bypassListenerEffects = true;
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void OnChangeVolume()
    {
        float masterVolume = GameDataManager.instance.saveData.option.masterVolume / 100.0f;
        float seVolume = GameDataManager.instance.saveData.option.seVolume / 100.0f;
        float bgmVolume = GameDataManager.instance.saveData.option.bgmVolume / 100.0f;
        bgmPlayer.volume = bgmVolume * masterVolume;
        foreach (AudioSource sfxPlayer in sfxPlayers)
        {
            sfxPlayer.volume = seVolume * masterVolume;
        }
    }
}
