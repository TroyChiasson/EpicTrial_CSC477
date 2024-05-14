using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SoundType {
    GUN = 0,
    LASER = 1,
    CHARGE = 2,
    BUTTON = 3,
    BOUNCE = 4,
    SHATTER = 5,
    BOOM = 6,
}

public class SoundCollection {
    private AudioClip[] clips;
    private string[] audioNames;

    public SoundCollection(params string[] audioNames) {
        this.audioNames = audioNames;
        clips = new AudioClip[audioNames.Length];
        for (int i = 0; i < clips.Length; i++) {
            clips[i] = Resources.Load(audioNames[i]) as AudioClip;
            if (clips[i] == null) {
                Debug.LogWarning($"Couldn't find clip {audioNames[i]}");
            }
        }
    }

    public AudioClip GetRandClip() {
        if (clips.Length == 0) {
            Debug.LogWarning($"No clips found");
            return null;
        }
        else if (clips.Length == 1) {
            return clips[0];
        }
        else {
            int index = UnityEngine.Random.Range(0, clips.Length);
            return clips[index];
        }
    }

    public string GetRandName() {
        if (clips.Length == 0) {
            Debug.LogWarning($"No clips found");
            return null;
        }
        else if (clips.Length == 1) {
            return audioNames[0];
        }
        else {
            int index = UnityEngine.Random.Range(0, clips.Length);
            return audioNames[index];
        }
    }
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
    private Dictionary<SoundType, SoundCollection> sounds;
    private AudioSource audioSrc;

    public static AudioManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        sounds = new Dictionary<SoundType, SoundCollection>()
        {
            {SoundType.GUN, new SoundCollection("gun") },
            {SoundType.LASER, new SoundCollection("laser") },
            {SoundType.CHARGE, new SoundCollection("charge") },
            {SoundType.BUTTON, new SoundCollection("button") },
            {SoundType.BOUNCE, new SoundCollection("bounce") },
            {SoundType.SHATTER, new SoundCollection("shatter") },
            {SoundType.BOOM, new SoundCollection("boom") },
        };
    }

    public void Play(SoundType type, AudioSource audioSrc = null) {
        if (sounds.ContainsKey(type)) {
            if (audioSrc == null) {
                this.audioSrc.clip = sounds[type].GetRandClip();
                this.audioSrc.PlayOneShot(sounds[type].GetRandClip());
            }
            else {
                audioSrc.clip = sounds[type].GetRandClip();
                audioSrc.PlayOneShot(sounds[type].GetRandClip());
            }
        }
    }

    public void Play(string type, AudioSource audioSrc) {
        SoundType soundType = Enum.Parse<SoundType>(type, true);
        Play(soundType, audioSrc);
    }

    public void Play(string type) {
        Play(type, audioSrc);
    }

    public void Play(int type) {
        Play((SoundType)type, audioSrc);
    }
}
