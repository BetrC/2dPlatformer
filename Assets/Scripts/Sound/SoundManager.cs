using System;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (var sound in sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            sound.Init(source);
        }
    }

    public void PlaySound(string name)
    {
        Sound sound = Array.Find(sounds, (s) => s.soundName == name);
        if (sound == null)
        {
            CLog.LogError($"sound: {name} not found in sounds");
            return;
        }
        sound.audioSource.Play();
    }
}
