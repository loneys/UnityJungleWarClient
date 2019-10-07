using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : BaseManager {

    public AudioManager(GameFacade facade) : base(facade) { }

    private const string Sound_Path_Prefix = "Sounds/";
    public const string Alert = "Alert";
    public const string ArrowShoot = "ArrowShoot";

    public const string Sound_Alert = "Alert";
    public const string Sound_ArrowShoot = "ArrowShoot";
    public const string Sound_Bg_Fast = "Bg(fast)";
    public const string Sound_Bg_Moderate = "Bg(moderate)";
    public const string Sound_ButtonClick = "ButtonClick";
    public const string Sound_Miss = "Miss";
    public const string Sound_ShootPerson = "ShootPerson";
    public const string Sound_Timer = "Timer";

    private AudioSource bgAudioSource;
    private AudioSource normalAudioSource;

    public override void OnInit()
    {
        GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
        bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
        normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

        PlaySound(bgAudioSource, LoadSound(Sound_Bg_Moderate), 0.5f, true);
    }

    public void PlaySoundBg(string soundName)
    {
        PlaySound(bgAudioSource, LoadSound(soundName), 0.5f, true);
    }

    public void PlayNormalSound(string SoundName)
    {
        PlaySound(normalAudioSource, LoadSound(SoundName), 0.5f, false);
    }

    private void PlaySound(AudioSource audioSource, AudioClip audioClip, float volume, bool loop = false)
    {
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private AudioClip LoadSound(string soundsName)
    {
        return Resources.Load<AudioClip>(Sound_Path_Prefix + soundsName);
    }

}
