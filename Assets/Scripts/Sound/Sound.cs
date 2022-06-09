using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class Sound
{
    public string soundName;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume = 1f;

    [Range(0.5f, 2)]
    public float pitch = 1f;

    public bool loop = false;

    [HideInInspector]
    public AudioSource audioSource;

    public void Init(AudioSource source)
    {
        source.clip = clip;
        source.loop = loop;
        source.volume = volume;
        source.pitch = pitch;
        audioSource = source;
    }

    public static Sound CreateFromAudioClip(AudioClip clip)
    {
        Sound sound = new Sound();
        sound.clip = clip;
        return sound;
    }
}
