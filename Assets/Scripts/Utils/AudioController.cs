﻿using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {
    public static AudioController _instance;
    public static AudioController SharedInstance {
        get {
            return _instance;
        }
    }

    private AudioSource bgmSource;
    private AudioSource[] soundEffects;

    [Range(1,8)] public int numberOfChannels = 1;

    void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        bgmSource.loop = true;

        soundEffects = new AudioSource[numberOfChannels];
        for(int i = 0; i < numberOfChannels; i++) {
            soundEffects[i] = gameObject.AddComponent<AudioSource>();
            soundEffects[i].loop = false;
            soundEffects[i].playOnAwake = false;
        }
    }

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
    }
	
    public void ChangeMusic(AudioClip clip, bool reset = true) {
        float time = (reset ? 0 : bgmSource.time);
        bgmSource.clip = clip;
        bgmSource.time = time;
        bgmSource.Play();
    }

    public void PlaySoundEffect(AudioClip sound, int channel) {
        if(channel < 0 || channel >= soundEffects.Length) return;
        AudioSource selected = soundEffects[channel];
        selected.clip = sound;
        selected.time = 0;
        selected.Play();
    }
}